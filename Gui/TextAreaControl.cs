// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SqlCodeEditor.Document;

namespace SqlCodeEditor
{
	/// <summary>
	/// This class paints the textarea.
	/// </summary>
	[ToolboxItem(false)]
	public class TextAreaControl : Panel
	{
		TextEditorControl motherTextEditorControl;
		
		HRuler     hRuler     = null;
		
		VScrollBar vScrollBar = new VScrollBar();
		HScrollBar hScrollBar = new HScrollBar();
		TextArea   textArea;
		bool       doHandleMousewheel = true;
		bool       disposed;
		
		public TextArea TextArea {
			get {
				return textArea;
			}
		}
		
		public SelectionManager SelectionManager {
			get {
				return textArea.SelectionManager;
			}
		}
		
		public Caret Caret {
			get {
				return textArea.Caret;
			}
		}
		
		[Browsable(false)]
		public IDocument Document {
			get {
				if (motherTextEditorControl != null)
					return motherTextEditorControl.Document;
				return null;
			}
		}
		
		public ITextEditorProperties TextEditorProperties {
			get {
				if (motherTextEditorControl != null)
					return motherTextEditorControl.TextEditorProperties;
				return null;
			}
		}
		
		public VScrollBar VScrollBar {
			get {
				return vScrollBar;
			}
		}
		
		public HScrollBar HScrollBar {
			get {
				return hScrollBar;
			}
		}
		
		public bool DoHandleMousewheel {
			get {
				return doHandleMousewheel;
			}
			set {
				doHandleMousewheel = value;
			}
		}
		
		public TextAreaControl(TextEditorControl motherTextEditorControl)
		{
			this.motherTextEditorControl = motherTextEditorControl;
			
			this.textArea                = new TextArea(motherTextEditorControl, this);
			Controls.Add(textArea);
			
			vScrollBar.ValueChanged += new EventHandler(VScrollBarValueChanged);
			Controls.Add(this.vScrollBar);
			
			hScrollBar.ValueChanged += new EventHandler(HScrollBarValueChanged);
			Controls.Add(this.hScrollBar);
			ResizeRedraw = true;
			
			Document.TextContentChanged += DocumentTextContentChanged;
			Document.DocumentChanged += AdjustScrollBarsOnDocumentChange;
			Document.UpdateCommited  += DocumentUpdateCommitted;
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (!disposed) {
					disposed = true;
					Document.TextContentChanged -= DocumentTextContentChanged;
					Document.DocumentChanged -= AdjustScrollBarsOnDocumentChange;
					Document.UpdateCommited  -= DocumentUpdateCommitted;
					motherTextEditorControl = null;
					if (vScrollBar != null) {
						vScrollBar.Dispose();
						vScrollBar = null;
					}
					if (hScrollBar != null) {
						hScrollBar.Dispose();
						hScrollBar = null;
					}
					if (hRuler != null) {
						hRuler.Dispose();
						hRuler = null;
					}
				}
			}
			base.Dispose(disposing);
		}
		
		void DocumentTextContentChanged(object sender, EventArgs e)
		{
			// after the text content is changed abruptly, we need to validate the
			// caret position - otherwise the caret position is invalid for a short amount
			// of time, which can break client code that expects that the caret position is always valid
			Caret.ValidateCaretPos();
		}
		
		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
            ResizeTextArea(true);
        }

        public void ResizeTextArea(bool forceRedraw = false)
        {
            var y = 0;
            var h = 0;
            if (hRuler != null)
            {
                hRuler.Bounds = new Rectangle(0,
                    0,
                    Width - SystemInformation.HorizontalScrollBarArrowWidth,
                    TextArea.TextView.FontHeight);

                y = hRuler.Bounds.Bottom;
                h = hRuler.Bounds.Height;
            }

            var fontHeight = TextArea.TextView.FontHeight;

            var totalLineHeight = GetNumberOfVisibleLines() * fontHeight;

            // If the lines cannot fit the TextArea draw the VScrollBar
            var drawVScrollBar = totalLineHeight > Height || TextArea.VirtualTop.Y > 0;

            var width = TextArea.TextView.DrawingPosition.Width;

            // Measuring string length is not exactly accurate, add 10 as a error margin
            var drawHScrollBar = width > 0 && GetMaximumVisibleLineWidth() + 10 > TextArea.TextView.DrawingPosition.Width ||
                                 TextArea.VirtualTop.X > 0;

            AdjustScrollBars();

            if (!forceRedraw && !IsRedrawRequired(drawVScrollBar, drawHScrollBar))
            {
                return;
            }

            VScrollBar.ValueChanged -= VScrollBarValueChanged;
            HScrollBar.ValueChanged -= HScrollBarValueChanged;

            if (drawHScrollBar && drawVScrollBar)
            {
                TextArea.Bounds = new Rectangle(0, y,
                    Width - SystemInformation.HorizontalScrollBarArrowWidth,
                    Height - SystemInformation.VerticalScrollBarArrowHeight - h);

                Controls.Remove(VScrollBar);
                Controls.Remove(HScrollBar);

                Controls.Add(VScrollBar);
                Controls.Add(HScrollBar);

                VScrollBar.ValueChanged += VScrollBarValueChanged;
                HScrollBar.ValueChanged += HScrollBarValueChanged;

                SetScrollBarBounds(true, true);
            }
            else if (drawVScrollBar)
            {
                TextArea.Bounds = new Rectangle(0, y,
                    Width - SystemInformation.HorizontalScrollBarArrowWidth,
                    Height);

                // If VScrollBar was not visible before scroll to the end
                if (!Controls.Contains(VScrollBar))
                    VScrollBar.Value = VScrollBar.Maximum;
                else
                    Controls.Remove(VScrollBar);

                Controls.Add(VScrollBar);
                Controls.Remove(HScrollBar);

                VScrollBar.ValueChanged += VScrollBarValueChanged;

                SetScrollBarBounds(true, false);
            }
            else if (drawHScrollBar)
            {
                TextArea.Bounds = new Rectangle(0, y,
                    Width,
                    Height - SystemInformation.VerticalScrollBarArrowHeight - h);

                Controls.Remove(HScrollBar);

                Controls.Add(HScrollBar);
                Controls.Remove(VScrollBar);

                HScrollBar.ValueChanged += HScrollBarValueChanged;

                SetScrollBarBounds(false, true);
            }
            else
            {
                Controls.Remove(VScrollBar);
                Controls.Remove(HScrollBar);

                TextArea.Bounds = new Rectangle(0, y,
                    Width,
                    Height);
            }

            TextArea.Invalidate();
        }

        public void SetScrollBarBounds(bool setVertical, bool setHorizontal)
        {
            if (setVertical)
            {
                VScrollBar.Bounds = new Rectangle(TextArea.Bounds.Right, 0, SystemInformation.HorizontalScrollBarArrowWidth,
                    setHorizontal ? Height - SystemInformation.VerticalScrollBarArrowHeight : Height);
                VScrollBar.Invalidate();
            }

            if (setHorizontal)
            {
                HScrollBar.Bounds = new Rectangle(0,
                    TextArea.Bounds.Bottom,
                    Width - SystemInformation.HorizontalScrollBarArrowWidth,
                    SystemInformation.VerticalScrollBarArrowHeight);
                HScrollBar.Invalidate();
            }
        }

        /// <summary>
        /// Gets the number of visible lines in the document by excluding lines that are not visible because of folding.
        /// </summary>
        /// <returns>The number of visible lines.</returns>
        private int GetNumberOfVisibleLines()
        {
            var lines = 0;

            for (var i = 0; i < Document.TotalNumberOfLines; i++)
            {
                if (Document.FoldingManager.IsLineVisible(i))
                {
                    lines++;
                }
            }

            return lines;
        }

        private int GetMaximumVisibleLineWidth()
        {
            var max = 0;
            using (var graphics = TextArea.CreateGraphics())
            {
                var firstLine = TextArea.TextView.FirstVisibleLine;
                var lastLine =
                    Document.GetFirstLogicalLine(TextArea.TextView.FirstPhysicalLine + TextArea.TextView.VisibleLineCount);
                if (lastLine >= Document.TotalNumberOfLines)
                    lastLine = Document.TotalNumberOfLines - 1;
                var tabIndent = Document.TextEditorProperties.TabIndent;
                var minTabWidth = 4;
                var wideSpaceWidth = TextArea.TextView.WideSpaceWidth;
                var fontContainer = TextEditorProperties.FontContainer;

                for (var lineNumber = firstLine; lineNumber <= lastLine; lineNumber++)
                {
                    var lineSegment = Document.GetLineSegment(lineNumber);

                    if (Document.FoldingManager.IsLineVisible(lineNumber))
                    {
                        var lineWidth = 0;
                        var words = lineSegment.Words;
                        var wordCount = words.Count;
                        var offset = 0;

                        for (var i = 0; i < wordCount; i++)
                        {
                            var word = words[i];

                            switch (word.Type)
                            {
                                case TextWordType.Space:
                                    lineWidth += TextArea.TextView.SpaceWidth;
                                    break;
                                case TextWordType.Tab:
                                    // go to next tab position
                                    lineWidth = (lineWidth + minTabWidth) / tabIndent / wideSpaceWidth * tabIndent * wideSpaceWidth;
                                    lineWidth += tabIndent * wideSpaceWidth;
                                    break;
                                case TextWordType.Word:
                                    var text = Document.GetText(offset + lineSegment.Offset, word.Length);

                                    lineWidth += TextArea.TextView.MeasureStringWidth(graphics, text,
                                        word.GetFont(fontContainer) ?? fontContainer.RegularFont);
                                    break;
                            }

                            offset += word.Length;
                        }

                        max = Math.Max(max, lineWidth);
                    }
                }
            }

            return max;
        }

        private bool IsRedrawRequired(bool drawVScrollBar, bool drawHScrollBar)
        {
            var vScrollBarVisible = Controls.Contains(VScrollBar);
            var hScrollBarVisible = Controls.Contains(HScrollBar);

            if ((drawVScrollBar && !vScrollBarVisible) || (!drawVScrollBar && vScrollBarVisible))
            {
                return true;
            }

            if ((drawHScrollBar && !hScrollBarVisible) || (!drawHScrollBar && hScrollBarVisible))
            {
                return true;
            }

            return false;
        }

        bool adjustScrollBarsOnNextUpdate;
		Point scrollToPosOnNextUpdate;
		
		void AdjustScrollBarsOnDocumentChange(object sender, DocumentEventArgs e)
		{
			if (motherTextEditorControl.IsInUpdate == false) {
				AdjustScrollBarsClearCache();
				AdjustScrollBars();
			} else {
				adjustScrollBarsOnNextUpdate = true;
			}
		}
		
		void DocumentUpdateCommitted(object sender, EventArgs e)
		{
			if (motherTextEditorControl.IsInUpdate == false) {
				Caret.ValidateCaretPos();
				
				// AdjustScrollBarsOnCommittedUpdate
				if (!scrollToPosOnNextUpdate.IsEmpty) {
					ScrollTo(scrollToPosOnNextUpdate.Y, scrollToPosOnNextUpdate.X);
				}
				if (adjustScrollBarsOnNextUpdate) {
					AdjustScrollBarsClearCache();
					AdjustScrollBars();
				}

                ResizeTextArea();
            }
		}
		
		int[] lineLengthCache;
		const int LineLengthCacheAdditionalSize = 100;
		
		void AdjustScrollBarsClearCache()
		{
			if (lineLengthCache != null) {
				if (lineLengthCache.Length < this.Document.TotalNumberOfLines + 2 * LineLengthCacheAdditionalSize) {
					lineLengthCache = null;
				} else {
					Array.Clear(lineLengthCache, 0, lineLengthCache.Length);
				}
			}
		}
		
		public void AdjustScrollBars()
		{
			adjustScrollBarsOnNextUpdate = false;
			vScrollBar.Minimum = 0;
			// number of visible lines in document (folding!)
			vScrollBar.Maximum = (Document.GetVisibleLine(Document.TotalNumberOfLines - 1) + 1) * TextArea.TextView.FontHeight; // ; textArea.MaxVScrollValue;
			int max = 0;
			
			int firstLine = textArea.TextView.FirstVisibleLine;
			int lastLine = this.Document.GetFirstLogicalLine(textArea.TextView.FirstPhysicalLine + textArea.TextView.VisibleLineCount);
			if (lastLine >= this.Document.TotalNumberOfLines)
				lastLine = this.Document.TotalNumberOfLines - 1;
			
			if (lineLengthCache == null || lineLengthCache.Length <= lastLine) {
				lineLengthCache = new int[lastLine + LineLengthCacheAdditionalSize];
			}
			
			for (int lineNumber = firstLine; lineNumber <= lastLine; lineNumber++) {
				LineSegment lineSegment = this.Document.GetLineSegment(lineNumber);
				if (Document.FoldingManager.IsLineVisible(lineNumber)) {
					if (lineLengthCache[lineNumber] > 0) {
						max = Math.Max(max, lineLengthCache[lineNumber]);
					} else {
						int visualLength = textArea.TextView.GetVisualColumnFast(lineSegment, lineSegment.Length);
						lineLengthCache[lineNumber] = Math.Max(1, visualLength);
						max = Math.Max(max, visualLength);
					}
				}
			}
			hScrollBar.Minimum = 0;
			hScrollBar.Maximum = (Math.Max(max + 3, textArea.TextView.VisibleColumnCount - 1));
			
			vScrollBar.LargeChange = Math.Max(0, textArea.TextView.DrawingPosition.Height);
			vScrollBar.SmallChange = Math.Max(0, textArea.TextView.FontHeight);
			
			hScrollBar.LargeChange = Math.Max(0, textArea.TextView.VisibleColumnCount - 1);
			hScrollBar.SmallChange = Math.Max(0, (int)textArea.TextView.SpaceWidth);
		}
		
		public void OptionsChanged()
		{
			textArea.OptionsChanged();
			
			if (textArea.TextEditorProperties.ShowHorizontalRuler) {
				if (hRuler == null) {
					hRuler = new HRuler(textArea);
					Controls.Add(hRuler);
					ResizeTextArea();
				} else {
					hRuler.Invalidate();
				}
			} else {
				if (hRuler != null) {
					Controls.Remove(hRuler);
					hRuler.Dispose();
					hRuler = null;
					ResizeTextArea();
				}
			}
			
			AdjustScrollBars();
		}
		
		void VScrollBarValueChanged(object sender, EventArgs e)
		{
			textArea.VirtualTop = new Point(textArea.VirtualTop.X, vScrollBar.Value);
            ResizeTextArea();
        }
		
		void HScrollBarValueChanged(object sender, EventArgs e)
		{
			textArea.VirtualTop = new Point(hScrollBar.Value * textArea.TextView.WideSpaceWidth, textArea.VirtualTop.Y);
            ResizeTextArea();
        }
		
		Util.MouseWheelHandler mouseWheelHandler = new Util.MouseWheelHandler();
		
		public void HandleMouseWheel(MouseEventArgs e)
		{
			int scrollDistance = mouseWheelHandler.GetScrollAmount(e);
			if (scrollDistance == 0)
				return;
			if ((Control.ModifierKeys & Keys.Control) != 0 && TextEditorProperties.MouseWheelTextZoom) {
				if (scrollDistance > 0) {
					motherTextEditorControl.Font = new Font(motherTextEditorControl.Font.Name,
					                                        motherTextEditorControl.Font.Size + 1);
				} else {
					motherTextEditorControl.Font = new Font(motherTextEditorControl.Font.Name,
					                                        Math.Max(6, motherTextEditorControl.Font.Size - 1));
				}
			} else {
				if (TextEditorProperties.MouseWheelScrollDown)
					scrollDistance = -scrollDistance;
				int newValue = vScrollBar.Value + vScrollBar.SmallChange * scrollDistance;
				vScrollBar.Value = Math.Max(vScrollBar.Minimum, Math.Min(vScrollBar.Maximum - vScrollBar.LargeChange + 1, newValue));
			}
		}
		
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (DoHandleMousewheel) {
				HandleMouseWheel(e);
			}
		}
		
		public void ScrollToCaret()
		{
			ScrollTo(textArea.Caret.Line, textArea.Caret.Column);
		}
		
		public void ScrollTo(int line, int column)
		{
			if (motherTextEditorControl.IsInUpdate) {
				scrollToPosOnNextUpdate = new Point(column, line);
				return;
			} else {
				scrollToPosOnNextUpdate = Point.Empty;
			}
			
			ScrollTo(line);
			
			int curCharMin  = (int)(this.hScrollBar.Value - this.hScrollBar.Minimum);
			int curCharMax  = curCharMin + textArea.TextView.VisibleColumnCount;
			
			int pos = textArea.TextView.GetVisualColumn(line, column);
			
			if (textArea.TextView.VisibleColumnCount < 0) {
				hScrollBar.Value = 0;
			} else {
				if (pos < curCharMin) {
					hScrollBar.Value = (int)(Math.Max(0, pos - scrollMarginHeight));
				} else {
					if (pos > curCharMax) {
						hScrollBar.Value = (int)Math.Max(0, Math.Min(hScrollBar.Maximum, (pos - textArea.TextView.VisibleColumnCount + scrollMarginHeight)));
					}
				}
			}
		}
		
		int scrollMarginHeight  = 3;
		
		/// <summary>
		/// Ensure that <paramref name="line"/> is visible.
		/// </summary>
		public void ScrollTo(int line)
		{
			line = Math.Max(0, Math.Min(Document.TotalNumberOfLines - 1, line));
			line = Document.GetVisibleLine(line);
			int curLineMin = textArea.TextView.FirstPhysicalLine;
			if (textArea.TextView.LineHeightRemainder > 0) {
				curLineMin ++;
			}
			
			if (line - scrollMarginHeight + 3 < curLineMin) {
				this.vScrollBar.Value =  Math.Max(0, Math.Min(this.vScrollBar.Maximum, (line - scrollMarginHeight + 3) * textArea.TextView.FontHeight)) ;
				VScrollBarValueChanged(this, EventArgs.Empty);
			} else {
				int curLineMax = curLineMin + this.textArea.TextView.VisibleLineCount;
				if (line + scrollMarginHeight - 1 > curLineMax) {
					if (this.textArea.TextView.VisibleLineCount == 1) {
						this.vScrollBar.Value =  Math.Max(0, Math.Min(this.vScrollBar.Maximum, (line - scrollMarginHeight - 1) * textArea.TextView.FontHeight)) ;
					} else {
						this.vScrollBar.Value = Math.Min(this.vScrollBar.Maximum,
						                                 (line - this.textArea.TextView.VisibleLineCount + scrollMarginHeight - 1)* textArea.TextView.FontHeight) ;
					}
					VScrollBarValueChanged(this, EventArgs.Empty);
				}
			}
		}
		
		/// <summary>
		/// Scroll so that the specified line is centered.
		/// </summary>
		/// <param name="line">Line to center view on</param>
		/// <param name="treshold">If this action would cause scrolling by less than or equal to
		/// <paramref name="treshold"/> lines in any direction, don't scroll.
		/// Use -1 to always center the view.</param>
		public void CenterViewOn(int line, int treshold)
		{
			line = Math.Max(0, Math.Min(Document.TotalNumberOfLines - 1, line));
			// convert line to visible line:
			line = Document.GetVisibleLine(line);
			// subtract half the visible line count
			line -= textArea.TextView.VisibleLineCount / 2;
			
			int curLineMin = textArea.TextView.FirstPhysicalLine;
			if (textArea.TextView.LineHeightRemainder > 0) {
				curLineMin ++;
			}
			if (Math.Abs(curLineMin - line) > treshold) {
				// scroll:
				this.vScrollBar.Value =  Math.Max(0, Math.Min(this.vScrollBar.Maximum, (line - scrollMarginHeight + 3) * textArea.TextView.FontHeight)) ;
				VScrollBarValueChanged(this, EventArgs.Empty);
			}
		}
		
		public void JumpTo(int line)
		{
			line = Math.Max(0, Math.Min(line, Document.TotalNumberOfLines - 1));
			string text = Document.GetText(Document.GetLineSegment(line));
			JumpTo(line, text.Length - text.TrimStart().Length);
		}
		
		public void JumpTo(int line, int column)
		{
			textArea.Focus();
			textArea.SelectionManager.ClearSelection();
			textArea.Caret.Position = new TextLocation(column, line);
			textArea.SetDesiredColumn();
			ScrollToCaret();
		}
		
		public event MouseEventHandler ShowContextMenu;
		
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x007B) { // handle WM_CONTEXTMENU
				if (ShowContextMenu != null) {
					long lParam = m.LParam.ToInt64();
					int x = unchecked((short)(lParam & 0xffff));
					int y = unchecked((short)((lParam & 0xffff0000) >> 16));
					if (x == -1 && y == -1) {
						Point pos = Caret.ScreenPosition;
						ShowContextMenu(this, new MouseEventArgs(MouseButtons.None, 0, pos.X, pos.Y + textArea.TextView.FontHeight, 0));
					} else {
						Point pos = PointToClient(new Point(x, y));
						ShowContextMenu(this, new MouseEventArgs(MouseButtons.Right, 1, pos.X, pos.Y, 0));
					}
				}
			}
			base.WndProc(ref m);
		}
		
		protected override void OnEnter(EventArgs e)
		{
			// SD2-1072 - Make sure the caret line is valid if anyone
			// has handlers for the Enter event.
			Caret.ValidateCaretPos();
			base.OnEnter(e);
		}
	}
}
