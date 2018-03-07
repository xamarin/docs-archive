using System;
using CoreGraphics;
using UIKit;
using Foundation;
using CoreText;

namespace CoreTextDrawing
{
    public class TextDrawingView : UIView
    {
        public TextDrawingView ()
        {
        }
        
        public override void Draw (CGRect rect)
        {
            base.Draw (rect);

            var gctx = UIGraphics.GetCurrentContext ();
            
            gctx.TranslateCTM (10, 0.5f * Bounds.Height);
            gctx.ScaleCTM (1, -1);
            gctx.RotateCTM ((float)Math.PI * 315 / 180);
 
            gctx.SetFillColor (UIColor.Green.CGColor);
            
            string someText = "你好世界";

            var attributedString = new NSAttributedString (someText,
                new CTStringAttributes{
                    ForegroundColorFromContext =  true,
                    Font = new CTFont ("Arial", 24)
                }); 
                
            using (var textLine = new CTLine (attributedString)) { 
                textLine.Draw (gctx);
            }      
        }
    }
}