#pragma checksum "D:\FU_Hub\SU23\PRN221\PROJECT\prn221_project_estoreapplication\eShopSolution.AdminApp\Views\Shared\_Layout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1930f82d623714fed3b9c06e507d06accd9ee927"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Layout), @"mvc.1.0.view", @"/Views/Shared/_Layout.cshtml")]
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
#line 1 "D:\FU_Hub\SU23\PRN221\PROJECT\prn221_project_estoreapplication\eShopSolution.AdminApp\Views\_ViewImports.cshtml"
using eShopSolution.AdminApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\FU_Hub\SU23\PRN221\PROJECT\prn221_project_estoreapplication\eShopSolution.AdminApp\Views\_ViewImports.cshtml"
using eShopSolution.AdminApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1930f82d623714fed3b9c06e507d06accd9ee927", @"/Views/Shared/_Layout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"30231d102d0f44c9eb29166031ec61d025e3e7c2", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__Layout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("sb-nav-fixed"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1930f82d623714fed3b9c06e507d06accd9ee9273784", async() => {
                WriteLiteral("\r\n    <meta charset=\"utf-8\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\" />\r\n    <meta name=\"description\"");
                BeginWriteAttribute("content", " content=\"", 256, "\"", 266, 0);
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <meta name=\"author\"");
                BeginWriteAttribute("content", " content=\"", 295, "\"", 305, 0);
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <title>");
#nullable restore
#line 9 "D:\FU_Hub\SU23\PRN221\PROJECT\prn221_project_estoreapplication\eShopSolution.AdminApp\Views\Shared\_Layout.cshtml"
      Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" - Administration</title>
    <link href=""/css/styles.css"" rel=""stylesheet"" />
    <link href=""https://cdn.datatables.net/1.10.20/css/dataTables.bootstrap4.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" />
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.11.2/js/all.min.js"" crossorigin=""anonymous""></script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1930f82d623714fed3b9c06e507d06accd9ee9275958", async() => {
                WriteLiteral("\r\n");
                WriteLiteral(@"    <div id=""layoutSidenav"">
        <div id=""layoutSidenav_nav"">
            <nav class=""sb-sidenav accordion sb-sidenav-dark"" id=""sidenavAccordion"">
                <div class=""sb-sidenav-menu"">
                    <div class=""nav"">
                        <div class=""sb-sidenav-menu-heading"">Hệ thống quản trị</div>
                        <a class=""nav-link"" href=""index.html"">
                            <div class=""sb-nav-link-icon""><i class=""fas fa-tachometer-alt""></i></div>
                            Trang chủ
                        </a>
                        <div class=""sb-sidenav-menu-heading"">Hệ thống</div>
                        <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#collapseLayouts"" aria-expanded=""false"" aria-controls=""collapseLayouts"">
                            <div class=""sb-nav-link-icon""><i class=""fas fa-columns""></i></div>
                            Người dùng
                            <div class=""sb-sidenav-collapse-arrow""><i class=");
                WriteLiteral(@"""fas fa-angle-down""></i></div>
                        </a>
                        <div class=""collapse"" id=""collapseLayouts"" aria-labelledby=""headingOne"" data-parent=""#sidenavAccordion"">
                            <nav class=""sb-sidenav-menu-nested nav"">
");
                WriteLiteral(@"                            </nav>
                        </div>
                        <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#collapsePages"" aria-expanded=""false"" aria-controls=""collapsePages"">
                            <div class=""sb-nav-link-icon""><i class=""fas fa-book-open""></i></div>
                            Danh mục
                            <div class=""sb-sidenav-collapse-arrow""><i class=""fas fa-angle-down""></i></div>
                        </a>
                        <div class=""collapse"" id=""collapsePages"" aria-labelledby=""headingTwo"" data-parent=""#sidenavAccordion"">
                            <nav class=""sb-sidenav-menu-nested nav accordion"" id=""sidenavAccordionPages"">
                                <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#pagesCollapseAuth"" aria-expanded=""false"" aria-controls=""pagesCollapseAuth"">
                                    Sản phẩm
                                    <div class=");
                WriteLiteral(@"""sb-sidenav-collapse-arrow""><i class=""fas fa-angle-down""></i></div>
                                </a>
                                <div class=""collapse"" id=""pagesCollapseAuth"" aria-labelledby=""headingOne"" data-parent=""#sidenavAccordionPages"">
                                    <nav class=""sb-sidenav-menu-nested nav"">
");
                WriteLiteral(@"                                    </nav>
                                </div>
                                <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#pagesCollapseError"" aria-expanded=""false"" aria-controls=""pagesCollapseError"">
                                    Error
                                    <div class=""sb-sidenav-collapse-arrow""><i class=""fas fa-angle-down""></i></div>
                                </a>
                                <div class=""collapse"" id=""pagesCollapseError"" aria-labelledby=""headingOne"" data-parent=""#sidenavAccordionPages"">
                                    <nav class=""sb-sidenav-menu-nested nav""><a class=""nav-link"" href=""401.html"">401 Page</a><a class=""nav-link"" href=""404.html"">404 Page</a><a class=""nav-link"" href=""500.html"">500 Page</a></nav>
                                </div>
                            </nav>
                        </div>
                        <div class=""sb-sidenav-menu-heading"">Addons</div>");
                WriteLiteral(@"
                        <a class=""nav-link"" href=""charts.html"">
                            <div class=""sb-nav-link-icon""><i class=""fas fa-chart-area""></i></div>
                            Charts
                        </a><a class=""nav-link"" href=""tables.html"">
                            <div class=""sb-nav-link-icon""><i class=""fas fa-table""></i></div>
                            Tables
                        </a>
                    </div>
                </div>
                <div class=""sb-sidenav-footer"">
                    <div class=""small"">Logged in as:</div>
                    Start Bootstrap
                </div>
            </nav>
        </div>
        <div id=""layoutSidenav_content"">
            <main>
                ");
#nullable restore
#line 81 "D:\FU_Hub\SU23\PRN221\PROJECT\prn221_project_estoreapplication\eShopSolution.AdminApp\Views\Shared\_Layout.cshtml"
           Write(RenderBody());

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
            </main>
            <footer class=""py-4 bg-light mt-auto"">
                <div class=""container-fluid"">
                    <div class=""d-flex align-items-center justify-content-between small"">
                        <div class=""text-muted"">Copyright &copy; Your Website 2019</div>
                        <div>
                            <a href=""#"">Privacy Policy</a>
                            &middot;
                            <a href=""#"">Terms &amp; Conditions</a>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src=""https://code.jquery.com/jquery-3.4.1.min.js"" crossorigin=""anonymous""></script>
    <script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.bundle.min.js"" crossorigin=""anonymous""></script>
    <script src=""/js/scripts.js""></script>

    ");
#nullable restore
#line 101 "D:\FU_Hub\SU23\PRN221\PROJECT\prn221_project_estoreapplication\eShopSolution.AdminApp\Views\Shared\_Layout.cshtml"
Write(RenderSection("Scripts", required: false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
