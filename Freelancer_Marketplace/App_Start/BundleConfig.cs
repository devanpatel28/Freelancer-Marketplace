using System.Web;
using System.Web.Optimization;

namespace Freelancer_Marketplace
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Logincss").Include(
                "~/Content/assets/vendor/fonts/fontawesome.css",
                "~/Content/assets/vendor/fonts/tabler-icons.css",
                "~/Content/assets/vendor/fonts/flag-icons.css",
                "~/Content/assets/vendor/css/rtl/core.css",
                "~/Content/assets/vendor/css/rtl/theme-default.css",
                "~/Content/assets/css/demo.css",
                "~/Content/assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css",
                "~/Content/assets/vendor/libs/node-waves/node-waves.css",
                "~/Content/assets/vendor/libs/typeahead-js/typeahead.css",
                "~/Content/assets/vendor/libs/formvalidation/dist/css/formValidation.min.css",
                "~/Content/assets/vendor/css/pages/page-auth.css"
            ));
            bundles.Add(new Bundle("~/bundles/script1").Include(
                    "~/Content/assets/vendor/js/helpers.js",
                    "~/Content/assets/vendor/js/template-customizer.js",
                    "~/Content/assets/js/config.js"));
            bundles.Add(new Bundle("~/bundles/Loginscript2").Include(
                    "~/Content/assets/vendor/libs/jquery/jquery.js",
                    "~/Content/assets/vendor/libs/popper/popper.js",
                    "~/Content/assets/vendor/js/bootstrap.js",
                    "~/Content/assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js",
                    "~/Content/assets/vendor/libs/node-waves/node-waves.js",
                    "~/Content/assets/vendor/libs/hammer/hammer.js",
                    "~/Content/assets/vendor/libs/i18n/i18n.js",
                    "~/Content/assets/vendor/libs/typeahead-js/typeahead.js",
                    "~/Content/assets/vendor/js/menu.js",
                    "~/Content/assets/vendor/libs/formvalidation/dist/js/FormValidation.min.js",
                    "~/Content/assets/vendor/libs/formvalidation/dist/js/plugins/Bootstrap5.min.js",
                    "~/Content/assets/vendor/libs/formvalidation/dist/js/plugins/AutoFocus.min.js",
                    "~/Content/assets/js/main.js",
                    "~/Content/assets/js/pages-auth.js"
                    ));
            bundles.Add(new StyleBundle("~/Content/maincss").Include(
                "~/Content/assets/vendor/fonts/fontawesome.css",
                "~/Content/assets/vendor/fonts/tabler-icons.css",
                "~/Content/assets/vendor/fonts/flag-icons.css",
                "~/Content/assets/vendor/css/rtl/core.css",
                "~/Content/assets/vendor/css/rtl/theme-default.css",
                "~/Content/assets/css/demo.css",
                "~/Content/assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css",
                "~/Content/assets/vendor/libs/node-waves/node-waves.css",
                "~/Content/assets/vendor/libs/typeahead-js/typeahead.css",
                "~/Content/assets/vendor/libs/select2/select2.css",
                "~/Content/assets/vendor/libs/apex-charts/apex-charts.css",
                "~/Content/assets/vendor/libs/formvalidation/dist/css/formValidation.min.css",
                "~/Content/assets/vendor/libs/swiper/swiper.css",
                "~/Content/assets/vendor/libs/animate-css/animate.css",
                "~/Content/assets/vendor/libs/sweetalert2/sweetalert2.css",
                "~/Content/assets/vendor/libs/datatables-bs5/datatables.bootstrap5.css",
                "~/Content/assets/vendor/libs/datatables-responsive-bs5/responsive.bootstrap5.css",
                "~/Content/assets/vendor/libs/datatables-checkboxes-jquery/datatables.checkboxes.css",
                "~/Content/assets/vendor/css/pages/cards-advance.css",
                "~/Content/assets/vendor/css/pages/app-chat.css"
                ));
            bundles.Add(new Bundle("~/bundles/mainscript2").Include(
                    "~/Content/assets/vendor/libs/jquery/jquery.js",
                    "~/Content/assets/vendor/libs/popper/popper.js",
                    "~/Content/assets/vendor/js/bootstrap.js",
                    "~/Content/assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js",
                    "~/Content/assets/vendor/libs/node-waves/node-waves.js",
                    "~/Content/assets/vendor/libs/hammer/hammer.js",
                    "~/Content/assets/vendor/libs/i18n/i18n.js",
                    "~/Content/assets/vendor/libs/typeahead-js/typeahead.js",
                    "~/Content/assets/vendor/js/menu.js",
                    "~/Content/assets/vendor/libs/select2/select2.js",
                    "~/Content/assets/vendor/libs/apex-charts/apexcharts.js",
                    "~/Content/assets/vendor/libs/swiper/swiper.js",
                    "~/Content/assets/vendor/libs/formvalidation/dist/js/FormValidation.min.js",
                    "~/Content/assets/vendor/libs/formvalidation/dist/js/plugins/Bootstrap5.min.js",
                    "~/Content/assets/vendor/libs/formvalidation/dist/js/plugins/AutoFocus.min.js",
                    "~/Content/assets/vendor/libs/cleavejs/cleave.js",
                    "~/Content/assets/vendor/libs/cleavejs/cleave-phone.js",
                    "~/Content/assets/vendor/libs/sweetalert2/sweetalert2.js",
                    "~/Content/assets/vendor/libs/datatables/jquery.dataTables.js",
                    "~/Content/assets/vendor/libs/datatables-bs5/datatables-bootstrap5.js",
                    "~/Content/assets/vendor/libs/datatables-responsive/datatables.responsive.js",
                    "~/Content/assets/vendor/libs/datatables-responsive-bs5/responsive.bootstrap5.js",
                    "~/Content/assets/vendor/libs/datatables-checkboxes-jquery/datatables.checkboxes.js",
                    "~/Content/assets/vendor/libs/chartjs/chartjs.js",
                    "~/Content/assets/vendor/libs/datatables-buttons/datatables-buttons.js",
                    "~/Content/assets/vendor/libs/datatables-buttons-bs5/buttons.bootstrap5.js",
                    "~/Content/assets/js/charts-chartjs.js",
                    "~/Content/assets/js/pages-account-settings-account.js",
                    "~/Content/assets/vendor/libs/jszip/jszip.js",
                    "~/Content/assets/vendor/libs/pdfmake/pdfmake.js",
                    "~/Content/assets/vendor/libs/datatables-buttons/buttons.html5.js",
                    "~/Content/assets/vendor/libs/datatables-buttons/buttons.print.js",
                    "~/Content/assets/js/main.js",
                    "~/Content/assets/js/dashboards-analytics.js",
                    "~/Content/assets/js/app-user-view.js",
                    "~/Content/assets/js/app-user-view-account.js",
                    "~/Content/assets/js/app-chat.js",
                    "~/Content/assets/js/modal-edit-user.js"
                    ));
        }
    }
}
