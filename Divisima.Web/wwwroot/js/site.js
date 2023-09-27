// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function getData() {
    $.ajax({
        type: "GET",
        url: "http://localhost:5153/api/home",
        success: function (data) {
            $(".apiData").empty();
            $(".apiData").append("<ul>")
            $.each(data, function (i, v) {
                $(".apiData").append("<li>"+v.name+"</li>")
            })
            $(".apiData").append("</ul>")
        },
        error: function (e) {
            alert(e.responseText)
        }
    });
}
