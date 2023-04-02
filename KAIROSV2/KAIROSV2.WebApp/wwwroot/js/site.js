// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    let sideNavMain = M.Sidenav.getInstance(document.getElementById("slide-out"));
    let fixOverlay = '<div id="fixed-overlay-menu" class="sidenav-overlay" style="display: block; opacity: 1;"></div>';

    $(window).on("resize", function () {

        if ($(window).width() > 959) {
            $(".menu-item-link").removeClass("sidenav-close");
        }

        if ($(window).width() < 960) {
            sideNavMain = M.Sidenav.getInstance(document.getElementById("slide-out"));
            $(".menu-item-link").addClass("sidenav-close");
        }
    });

    $("#main-sidenav-trigger").on("click", function (e) {
        e.preventDefault();
        e.stopPropagation();

        if ($(window).width() < 960) {
            if (sideNavMain.isOpen) {
            }
            else {
                $("body").append(fixOverlay)
                $("#fixed-overlay-menu").on("click", () => {
                    sideNavMain.close();
                    $("#fixed-overlay-menu").off("click", "**");
                    $("#fixed-overlay-menu").remove();
                });
                sideNavMain.open();
            }
        }
        return false;
    });

});