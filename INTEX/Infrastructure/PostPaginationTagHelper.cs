using System;
using INTEX.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace INTEX.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "post-page-model")]
    public class PostPaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory uhf;

        public PostPaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PostPageModel { get; set; }
        public string PostPageAction { get; set; }
        public string PostPageClass { get; set; }
        public bool PostPageClassEnabled { get; set; }
        public string PostPageClassNormal { get; set; }
        public string PostPageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            if (PostPageModel.TotalPages < 11)
            {
                for (int i = 1; i <= PostPageModel.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("button");
                    tb.Attributes["formaction"] = uh.Action(PostPageAction, new { pageNum = i });
                    tb.Attributes["type"] = "submit";

                    if (PostPageClassEnabled)
                    {
                        tb.AddCssClass(PostPageClass);
                        tb.AddCssClass(i == PostPageModel.CurrentPage ? PostPageClassSelected : PostPageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
            }
            else
            {
                if (PostPageModel.CurrentPage != 1)
                {
                    TagBuilder left = new TagBuilder("button");
                    left.Attributes["formaction"] = uh.Action(PostPageAction, new { pageNum = PostPageModel.CurrentPage - 1 });
                    left.Attributes["type"] = "submit";
                    left.AddCssClass(PostPageClass);
                    left.AddCssClass(PostPageClassNormal);
                    left.InnerHtml.Append("<");
                    final.InnerHtml.AppendHtml(left);
                }
                for (int i = 1; i <= 3; i++)
                {
                    TagBuilder tb = new TagBuilder("button");
                    tb.Attributes["formaction"] = uh.Action(PostPageAction, new { pageNum = i });
                    tb.Attributes["type"] = "submit";

                    if (PostPageClassEnabled)
                    {
                        tb.AddCssClass(PostPageClass);
                        tb.AddCssClass(i == PostPageModel.CurrentPage ? PostPageClassSelected : PostPageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }

                TagBuilder dots = new TagBuilder("button");
                dots.AddCssClass(PostPageClass);
                dots.AddCssClass(PostPageClassNormal);
                dots.Attributes["disabled"] = "true";
                dots.InnerHtml.Append("...");
                final.InnerHtml.AppendHtml(dots);

                if (PostPageModel.CurrentPage > 3 && PostPageModel.CurrentPage < PostPageModel.TotalPages - 3)
                {
                    for (int i = PostPageModel.CurrentPage - 1; i <= PostPageModel.CurrentPage + 1; i++)
                    {
                        TagBuilder tb = new TagBuilder("button");
                        tb.Attributes["formaction"] = uh.Action(PostPageAction, new { pageNum = i });
                        tb.Attributes["type"] = "submit";

                        if (PostPageClassEnabled)
                        {
                            tb.AddCssClass(PostPageClass);
                            tb.AddCssClass(i == PostPageModel.CurrentPage ? PostPageClassSelected : PostPageClassNormal);
                        }
                        tb.InnerHtml.Append(i.ToString());

                        final.InnerHtml.AppendHtml(tb);
                    }

                    final.InnerHtml.AppendHtml(dots);
                }
                else
                {
                    for (int i = (PostPageModel.TotalPages / 2) - 1; i <= (PostPageModel.TotalPages / 2) + 1; i++)
                    {
                        TagBuilder tb = new TagBuilder("button");
                        tb.Attributes["formaction"] = uh.Action(PostPageAction, new { pageNum = i });
                        tb.Attributes["type"] = "submit";

                        if (PostPageClassEnabled)
                        {
                            tb.AddCssClass(PostPageClass);
                            tb.AddCssClass(i == PostPageModel.CurrentPage ? PostPageClassSelected : PostPageClassNormal);
                        }
                        tb.InnerHtml.Append(i.ToString());

                        final.InnerHtml.AppendHtml(tb);
                    }

                    final.InnerHtml.AppendHtml(dots);
                }

                for (int i = PostPageModel.TotalPages - 1; i <= PostPageModel.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("button");
                    tb.Attributes["formaction"] = uh.Action(PostPageAction, new { pageNum = i });
                    tb.Attributes["type"] = "submit";

                    if (PostPageClassEnabled)
                    {
                        tb.AddCssClass(PostPageClass);
                        tb.AddCssClass(i == PostPageModel.CurrentPage ? PostPageClassSelected : PostPageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }

                if (PostPageModel.CurrentPage != PostPageModel.TotalPages)
                {
                    TagBuilder right = new TagBuilder("button");
                    right.Attributes["formaction"] = uh.Action(PostPageAction, new { pageNum = PostPageModel.CurrentPage + 1 });
                    right.Attributes["type"] = "submit";
                    right.AddCssClass(PostPageClass);
                    right.AddCssClass(PostPageClassNormal);
                    right.InnerHtml.Append(">");
                    final.InnerHtml.AppendHtml(right);
                }
            }

            output.Content.AppendHtml(final.InnerHtml);
        }
    }
}
