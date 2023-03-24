using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dialogue_Visualizer.Helpers.Tags
{
    public class ContextMenuTagHelper : TagHelper
    {
        public string BlockId { get; set; } = string.Empty;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "menu";

            var blockid = "dialogue-block-" + BlockId;
            output.Attributes.Add("blockid", blockid);
        }
    }
}
