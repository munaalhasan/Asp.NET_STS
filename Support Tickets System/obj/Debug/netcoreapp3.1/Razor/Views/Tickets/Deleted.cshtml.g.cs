#pragma checksum "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\Tickets\Deleted.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3d6837a6c9ffeb866df10198e189fa1f8fe5e16b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tickets_Deleted), @"mvc.1.0.view", @"/Views/Tickets/Deleted.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\_ViewImports.cshtml"
using Support_Tickets_System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\_ViewImports.cshtml"
using Support_Tickets_System.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3d6837a6c9ffeb866df10198e189fa1f8fe5e16b", @"/Views/Tickets/Deleted.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"28a0289349a3e89ff2cbbe8a36296d3c2a09686b", @"/Views/_ViewImports.cshtml")]
    public class Views_Tickets_Deleted : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SupportTicketsSystem.Website.Models.Tickets.TicketViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\Tickets\Deleted.cshtml"
  
    ViewData["Title"] = "Deleted";
    Layout = "_LayoutWithLeftMenuTickets";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Deleted Tickets List</h1>

<table class=""table"">
    <thead>
        <tr>
            <th>
                Description
            </th>
            <th>
                SerialNumber
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 23 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\Tickets\Deleted.cshtml"
         foreach (var item in Model.TicketsList)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 27 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\Tickets\Deleted.cshtml"
               Write(Html.DisplayFor(modelItem => item.TicketDesc));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 30 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\Tickets\Deleted.cshtml"
               Write(Html.DisplayFor(modelItem => item.TicketSerialNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 33 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\Tickets\Deleted.cshtml"
               Write(Html.ActionLink("Restore", "Restore", new { deletedTicketId = item.TicketId }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 36 "C:\Users\DELL\source\repos\Support Tickets System\Support Tickets System\Views\Tickets\Deleted.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SupportTicketsSystem.Website.Models.Tickets.TicketViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
