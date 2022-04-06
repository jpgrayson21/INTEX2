using System;
using INTEX.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace INTEX.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public bool PageClassEnabled { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            if (PageModel.TotalPages < 11)
            {
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(new UrlActionContext{ Action = PageAction, Controller = "Home", Values = new { pageNum = i }});

                    if (PageClassEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
            }
            else
            {
                if (PageModel.CurrentPage != 1)
                {
                    TagBuilder left = new TagBuilder("a");
                    left.Attributes["href"] = uh.Action(PageAction, "Home", new { pageNum = PageModel.CurrentPage - 1 });
                    left.AddCssClass(PageClass);
                    left.AddCssClass(PageClassNormal);
                    left.InnerHtml.Append("<");
                    final.InnerHtml.AppendHtml(left);
                }
                for (int i = 1; i <= 3; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, "Home", new { pageNum = i });

                    if (PageClassEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }

                TagBuilder dots = new TagBuilder("button");
                dots.AddCssClass(PageClass);
                dots.AddCssClass(PageClassNormal);
                dots.Attributes["disabled"] = "true";
                dots.InnerHtml.Append("...");
                final.InnerHtml.AppendHtml(dots);

                if (PageModel.CurrentPage > 3 && PageModel.CurrentPage < PageModel.TotalPages - 3)
                {
                    for (int i = PageModel.CurrentPage - 1; i <= PageModel.CurrentPage + 1; i++)
                    {
                        TagBuilder tb = new TagBuilder("a");
                        tb.Attributes["href"] = uh.Action(PageAction, "Home", new { pageNum = i });

                        if (PageClassEnabled)
                        {
                            tb.AddCssClass(PageClass);
                            tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                        }
                        tb.InnerHtml.Append(i.ToString());

                        final.InnerHtml.AppendHtml(tb);
                    }

                    final.InnerHtml.AppendHtml(dots);
                }
                else
                {
                    for (int i = (PageModel.TotalPages / 2) - 1; i <= (PageModel.TotalPages / 2) + 1; i++)
                    {
                        TagBuilder tb = new TagBuilder("a");
                        tb.Attributes["href"] = uh.Action(PageAction, "Home", new { pageNum = i });

                        if (PageClassEnabled)
                        {
                            tb.AddCssClass(PageClass);
                            tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                        }
                        tb.InnerHtml.Append(i.ToString());

                        final.InnerHtml.AppendHtml(tb);
                    }

                    final.InnerHtml.AppendHtml(dots);
                }

                for (int i = PageModel.TotalPages - 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, "Home", new { pageNum = i });

                    if (PageClassEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }

                if (PageModel.CurrentPage != PageModel.TotalPages)
                {
                    TagBuilder right = new TagBuilder("a");
                    right.Attributes["href"] = uh.Action(new UrlActionContext { Action = PageAction, Controller = "Home", Values = new { pageNum = PageModel.CurrentPage + 1 } });
                    right.AddCssClass(PageClass);
                    right.AddCssClass(PageClassNormal);
                    right.InnerHtml.Append(">");
                    final.InnerHtml.AppendHtml(right);
                }
            }

            output.Content.AppendHtml(final.InnerHtml);
        }
    }
}
