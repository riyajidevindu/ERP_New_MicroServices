using ERP.TranscriptGenetation.Core.Entity;
using ERP.TranscriptGenetation.Core.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TranscriptGeneration.Application
{
    

    public class TranscriptDocument (IUnitOfWork unitOfWork) : IDocument
    {
        public static Image LogoImage { get; } = Image.FromFile(@"c:\temp\uor_logo.png");

       

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"Transcript Faculty of Engineering")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("Issue date: ").SemiBold();
                        text.Span($"");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Due date: ").SemiBold();
                        text.Span($"");
                    });
                });

                row.ConstantItem(175).Image(LogoImage);
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(20);

                column.Item().Row(row =>
                {
                   
                });

                column.Item().Element(ComposeTable);

                
            });
        }

        void ComposeTable(IContainer container)
        {
            

            var students = unitOfWork.Students.GetAllAsync().Result;

            var results = unitOfWork.Results.GetAllAsync().Result;
            var headerStyle = TextStyle.Default.SemiBold();
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(10);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(3);
                });

                table.Header(header =>
                {
                    header.Cell().Text("#");
                    header.Cell().Text("Module").Style(headerStyle);
                    header.Cell().AlignCenter().Text("Module Name").Style(headerStyle);
                    header.Cell().AlignCenter().Text("Grade").Style(headerStyle);
                    header.Cell().AlignCenter().Text("GPV").Style(headerStyle);

                    header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
                int i = 0;
                foreach (var item in results)
                {
                    var index = ++i;

                    table.Cell().Element(CellStyle).Text($"{index}");
                    table.Cell().Element(CellStyle).Text(item.ModuleCode);
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.ModuleName);
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Grade);
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.GPV);

                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            });
        }

        void ComposeComments(IContainer container)
        {
            container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(14).SemiBold();
               
            });
        }
    }

    
}
