System.register(["jquery", "react-dom", "react", "./Button"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __extends = (this && this.__extends) || function (d, b) {
        for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
    var jquery_1, ReactDOM, React, Button_1;
    var Taskboard;
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
            Taskboard = (function (_super) {
                __extends(Taskboard, _super);
                function Taskboard() {
                    _super.apply(this, arguments);
                }
                Taskboard.prototype.render = function () {
                    return React.createElement("div", null, React.createElement(Button_1.default, {id: "addTask", text: "Task"}));
                };
                return Taskboard;
            }(React.Component));
            //This kicks everything off
            jquery_1.default(function () {
                ReactDOM.render(React.createElement(Taskboard, null), jquery_1.default('#app')[0]);
            });
        }
    }
});
//# sourceMappingURL=main.js.map