#pragma checksum "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07aba5af4f8338d8559a53a7421d22ace20b3e2a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServiceProvider_NewServiceRequest), @"mvc.1.0.view", @"/Views/ServiceProvider/NewServiceRequest.cshtml")]
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
#line 1 "E:\HelperlandNew\Views\_ViewImports.cshtml"
using Helperland;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\HelperlandNew\Views\_ViewImports.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07aba5af4f8338d8559a53a7421d22ace20b3e2a", @"/Views/ServiceProvider/NewServiceRequest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5f94cf04a7ec23f27ac33992ef127038e0b3154", @"/Views/_ViewImports.cshtml")]
    public class Views_ServiceProvider_NewServiceRequest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Helperland.Models.ViewModel.NewServiceRequestViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/servicehistory.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_partialLeftSideNavForSp", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NewServiceRequest", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ServiceProvider", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "/Images/calendar2.png", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
  
    Layout = "_SpLayout";
    ViewBag.Title = "New Service Request ";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "07aba5af4f8338d8559a53a7421d22ace20b3e2a6306", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral("<div class=\"container-fluid\">\r\n    \r\n    <div class=\"row justify-content-center mt-2\">\r\n        <div class=\"col-md-3\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "07aba5af4f8338d8559a53a7421d22ace20b3e2a7597", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 14 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
          
            bool haspate = ViewBag.hasPat;
        

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""col-md-9"">
            <div class=""alert alert-success display-none"" role=""alert"" id=""alertSuccessMessage"">
                Request Accepted Successfully.
            </div>
            <div class=""alert alert-danger display-none"" role=""alert"" id=""alertErrorMessage"">

            </div>
            <div class=""row"">
                <input type=""hidden""");
            BeginWriteAttribute("value", " value=\"", 884, "\"", 900, 1);
#nullable restore
#line 25 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
WriteAttributeValue("", 892, haspate, 892, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"txtHiddenHaspate\" />\r\n                <div class=\"col-md-2\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07aba5af4f8338d8559a53a7421d22ace20b3e2a9809", async() => {
                WriteLiteral("\r\n                        <input type=\"checkbox\" name=\"hasPate\" id=\"cbHaspate\" /> <span class=\"font-bold\"> Have Pets at Home </span>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                </div>
            </div>
            <div class=""row p-3"">
                <div class=""col-md-12 table-responsive"">
                    <table id=""tbleNewServiceRequest"" class=""table"">
                        <thead>
                            <tr>
                                <td>Service Id </td>
                                <td>Service Date </td>
                                <td>Customer Details </td>
                                <td>Payment</td>
                                <td> Action</td>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 45 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td");
            BeginWriteAttribute("onclick", " onclick=\"", 2046, "\"", 2089, 3);
            WriteAttributeValue("", 2056, "OpenModel(", 2056, 10, true);
#nullable restore
#line 48 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
WriteAttributeValue("", 2066, item.ServiceRequestID, 2066, 22, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2088, ")", 2088, 1, true);
            EndWriteAttribute();
            WriteLiteral("> ");
#nullable restore
#line 48 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
                                                                                Write(item.ServiceRequestID);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                                    <td class=\"flex\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2174, "\"", 2217, 3);
            WriteAttributeValue("", 2184, "OpenModel(", 2184, 10, true);
#nullable restore
#line 49 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
WriteAttributeValue("", 2194, item.ServiceRequestID, 2194, 22, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2216, ")", 2216, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <div>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "07aba5af4f8338d8559a53a7421d22ace20b3e2a14208", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
#nullable restore
#line 50 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("<b> ");
#nullable restore
#line 50 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
                                                                                                       Write(item.ServicestartDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> </div>\r\n                                        <img src=\"/Images/layer-712.png\" /> 8.:00 - 12:00\r\n                                    </td>\r\n                                    <td");
            BeginWriteAttribute("onclick", " onclick=\"", 2537, "\"", 2580, 3);
            WriteAttributeValue("", 2547, "OpenModel(", 2547, 10, true);
#nullable restore
#line 53 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
WriteAttributeValue("", 2557, item.ServiceRequestID, 2557, 22, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2579, ")", 2579, 1, true);
            EndWriteAttribute();
            WriteLiteral(@">

                                        <div class=""d-flex"">
                                            <div class=""center""><img src=""/Images/layer-15.png""></div>
                                            <div>
                                                <span class=""d-block""> ");
#nullable restore
#line 58 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
                                                                  Write(item.CustomerName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\r\n                                                <span class=\"d-block\"> ");
#nullable restore
#line 59 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
                                                                  Write(item.Addressline1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\r\n                                                <span class=\"d-block\"> ");
#nullable restore
#line 60 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
                                                                  Write(item.Addressline2);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\r\n\r\n                                            </div>\r\n\r\n                                        </div>\r\n\r\n\r\n                                    </td>\r\n                                    <td class=\"active-font\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3310, "\"", 3353, 3);
            WriteAttributeValue("", 3320, "OpenModel(", 3320, 10, true);
#nullable restore
#line 68 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
WriteAttributeValue("", 3330, item.ServiceRequestID, 3330, 22, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3352, ")", 3352, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <div> <b> ");
#nullable restore
#line 69 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
                                             Write(item.Payment);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"  ???</b> </div>
                                    </td>
                                    <td>

                                        <button class=""btn btn-success btn-sm font-white rounded-pill m-2"" ata-bs-toggle=""modal"" data-bs-target=""#ServiceDeatils""");
            BeginWriteAttribute("onclick", " onclick=\"", 3684, "\"", 3731, 3);
            WriteAttributeValue("", 3694, "AcceptRequest(", 3694, 14, true);
#nullable restore
#line 73 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
WriteAttributeValue("", 3708, item.ServiceRequestID, 3708, 22, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3730, ")", 3730, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Accept</button>\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 76 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>


<!-- Modal  Service Details -->
<div class=""modal fade"" id=""ServiceDeatils"" tabindex=""-1"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered "">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Service Details</h5>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
            </div>
            <div class=""modal-body"">
                <div class=""row"">
                    <div class=""col-md-12"" id=""ModelMainDiv"">
                        <div>
                            <span id=""ModelServiceDate"" class=""font-bold h5""></span> 8:00 - 13:00<br />
                            <span class=""font-bold""> Duration </span><span id=""ModelDuration""> </span> Hrs
                        ");
            WriteLiteral(@"</div>
                        <hr />
                        <div>
                            <span class=""font-bold""> Service Id: </span><span id=""ModelServiceRequestId""> </span>.<br />
                            <span class=""font-bold""> Extras </span> <span id=""ModelExtra""></span><br />
                            <span class=""font-bold""> Net Amount: </span> <span id=""ModelTotalCost"" class=""font-blue font-bold""></span> ???<br />
                        </div>
                        <hr />
                        <div>
                            <span class=""font-bold"">Customer Name: </span><span id=""ModelCustomerName""> </span><br />
                            <span class=""font-bold""> Service Address :-  </span><span id=""ModelServiceAddress""> </span>.

                        </div>
                        <hr />
                        <div>
                            <span class=""font-bold""> Comments </span> <br /> <span id=""ModelComments""> </span>
                        </div>
     ");
            WriteLiteral("                   <hr />\r\n                        <span id=\"ModelHasPat\"> </span>\r\n                        <hr />\r\n                        <button class=\"btn btn-success font-white rounded-pill m-2\" id=\"ModelbtnAccept\"");
            BeginWriteAttribute("value", " value=\"", 6130, "\"", 6138, 0);
            EndWriteAttribute();
            WriteLiteral(@" onclick=""AcceptRequest(this.value)"">Accept </button>

                    </div>
                </div>
            </div>

        </div>
    </div>
</div>


    <link href=""https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css"" rel=""stylesheet"">
    <script src=""https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js""></script>
    <script>
        $(document).ready(function () {
            let cb = $(""#txtHiddenHaspate"").val();
            if (cb) {
                $('#cbHaspate').prop('checked', true);
            }
            else {
                $('#cbHaspate').prop('checked', false);
            }
            $('#cbHaspate').change(function () {
                $(this).closest('form').submit();
            });
           /* Apply Data Table*/
            $('#tbleNewServiceRequest').dataTable({
                ""bFilter"": false,
                ""bInfo"": false,
                'aoColumnDefs': [{
                    'bSortable': false,
                   ");
            WriteLiteral(@" 'aTargets': [-1] /* 1st one, start by the right */
                }]
            });
        });
           /*   Open Service Details Model on Click of Table Row*/
        function OpenModel(para) {
            $(""#ServiceDeatils"").modal('show');
            $(""#ModelServiceRequestId"").text(para);
            $(""#ModelbtnAccept"").val(para);
        $.ajax({
                  type: ""POST"",
            url: """);
#nullable restore
#line 162 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
             Write(Url.Action("GetServiceRequestData"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data: { ""ServiceReqestId"": para },
                 success: function (responce) {
                     var obj = JSON.parse(responce);
                     $(""#ModelDuration"").text(obj.TotalHour);
                     $(""#ModelServiceDate"").text(obj.ServiceDate);
                     $(""#ModelExtra"").text(obj.Extra);
                     $(""#ModelTotalCost"").text(obj.TotalCost);
                     $(""#ModelServiceAddress"").text(obj.Address);
                     $(""#ModelCustomerName"").text(obj.CustomerName);
                     $(""#ModelComments"").text(obj.Comments);
                     if (obj.Haspet) {
                        /* $(""#ModelHasPatImage"").attr(""src"", ""/Images/included.png"");*/
                         $(""#ModelHasPat"").text("" ??? I have pets at Home"");
                     } else {
                         /*$(""#ModelHasPatImage"").attr(""src"", ""/Images/not-included.png"");*/
                         $(""#ModelHasPat"").text("" ??? I don't have pets at Home"")
       ");
            WriteLiteral(@"              }
                 },
                 failure: function (response) {
                        alert(""failure"");
                 },
                 error: function (response) {
                        alert(""Something went Worng"");
                 }
              });
        }
        function AcceptRequest(para) {
            $(""#ServiceDeatils"").modal('hide');
             $.ajax({
                  type: ""POST"",
            url: """);
#nullable restore
#line 193 "E:\HelperlandNew\Views\ServiceProvider\NewServiceRequest.cshtml"
             Write(Url.Action("AcceptServiceRequest"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                 data: { ""serviceRequeestId"": para },
                 success: function (responce) {
                     if (responce == ""true"") {
                         $(""#alertSuccessMessage"").show().fadeOut(8000);
                         setTimeout(ReloadPage, 3000);
                     } else {
                         $(""#alertErrorMessage"").show().text(responce).fadeOut(10000);
                     }
                 },
                 failure: function (response) {
                        alert(""failure"");
                 },
                 error: function (response) {
                        alert(""Something went Worng"");
                 }
              });
        }
        function ReloadPage() {
            location.reload();
        }
    </script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Helperland.Models.ViewModel.NewServiceRequestViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
