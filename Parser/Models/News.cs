using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;

namespace Parser.Models
{
    class News
    {
        public string Header { get; set; }

        public string Text { get; set; }

        public string Date { get; set; }

        public string Url { get; set; }

        public FlowDocument GetAsFlowDocument()
        {
            FlowDocument doc = new FlowDocument();

            Paragraph p = new Paragraph(new Run(Header));
            p.FontSize = 36;
            doc.Blocks.Add(p);

            p = new Paragraph(new Run(Date));
            p.FontSize = 12;
            p.FontStyle = FontStyles.Italic;
            p.TextAlignment = TextAlignment.Right;
            p.Foreground = Brushes.Gray;
            doc.Blocks.Add(p);

            p = new Paragraph(new Run(Url));
            p.FontSize = 15;
            p.FontStyle = FontStyles.Normal;
            p.TextAlignment = TextAlignment.Right;
            p.Foreground = Brushes.Gray;
            doc.Blocks.Add(p);

            p = new Paragraph(new Run(Text));
            p.FontSize = 14;
            p.FontStyle = FontStyles.Normal;
            p.TextAlignment = TextAlignment.Left;
            p.Foreground = Brushes.Gray;
            doc.Blocks.Add(p);

            return doc;
        }
    }
}
