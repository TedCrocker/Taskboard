System.register(["jquery", "react-dom", "react", "./Button"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var jquery_1, ReactDOM, React, Button_1;
    return {
        setters:[
            function (jquery_1_1) {
                jquery_1 = jquery_1_1;
            },
            function (ReactDOM_1) {
                ReactDOM = ReactDOM_1;
            },
            function (React_1) {
                React = React_1;
            },
            function (Button_1_1) {
                Button_1 = Button_1_1;
            }],
        execute: function() {
            //This kicks everything off
            jquery_1.default(function () {
                ReactDOM.render(React.createElement(Button_1.default, null), jquery_1.default('#app')[0]);
            });
        }
    }
});
//# sourceMappingURL=main.js.map