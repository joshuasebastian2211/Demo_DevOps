﻿var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('SampleBoilerTemp');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);