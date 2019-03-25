var wine = wine || {};

wine.Example = wine.Example || (function () {
    function init() {
        $("html").removeClass("no-js").addClass("js");
    }
    return {
        init: init
    };
}());

$(document).ready(function () {
    window.wine.Example.init();
});
