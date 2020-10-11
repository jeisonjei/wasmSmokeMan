window.funcs = {
    pop: function () { $('.pop').popover() },
    selectpicker: function () { $(".selectpicker").selectpicker() },
    tooltip: function () { $('[data-toggle="tooltip"]').tooltip({ trigger: 'hover' }) },
    scrollResultDown: function () {
        let elem = document.getElementById('results');
        $(elem).scrollTop(3000);
    }
}