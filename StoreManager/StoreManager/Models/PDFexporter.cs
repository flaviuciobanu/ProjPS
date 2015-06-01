using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
namespace StoreManager.Models
{
    public class PDFexporter
    {
        public static void exportPDF(String invoice)
        { 
        // Create a new PDF document

        PdfDocument document = new PdfDocument();

        document.Info.Title = "Invoice";

                // Create an empty page
                PdfPage page = document.AddPage();
                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);
                // Create a font
                XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

                // Draw the text
                gfx.DrawString(invoice, font, XBrushes.Black,

                new XRect(0, 0, page.Width, page.Height),

                XStringFormats.Center);

                // Save the document...

                const string filename = "Invoice.pdf";

                document.Save(filename);

                // ...and start a viewer.

                Process.Start(filename);
        }
    }
}