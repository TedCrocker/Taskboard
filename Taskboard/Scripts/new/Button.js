System.register(["react"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __extends = (this && this.__extends) || function (d, b) {
        for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
    var React;
    var Button;
    return {
        setters:[
            function (React_1) {
                React = React_1;
            }],
        execute: function() {
            Button = (function (_super) {
                __extends(Button, _super);
                function Button() {
                    _super.apply(this, arguments);
                }
                Button.prototype.render = function () {
                    return React.createElement("button", {className: "button"});
                };
                return Button;
            }(React.Component));
            exports_1("default", Button);
        }
    }
});
//# sourceMappingURL=Button.js.map