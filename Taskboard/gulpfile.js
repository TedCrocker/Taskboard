/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var rename = require('gulp-rename');

var mappings = [
	{ "src": "jquery/dist/jquery.js", "destName": "jquery.js" },
	{
		"js": [{ "src": "jquery-ui/jquery-ui.js" }],
		"js": [{ "src": "signalr/jquery.signalR.js"}],
		"css": [
			{ "src": "jquery-ui/themes/base/*", destFolder: "jquery-ui" },
			{ "src": "jquery-ui/themes/base/images/*", destFolder: "jquery-ui/images" }
		]
	}
];

gulp.task('default', function () {
	var BASE_DIR = "./wwwroot/lib/";
	var JS_DEST = "./Scripts/thirdparty/";
	var CSS_DEST = "./Content/thirdparty/";

	function copyFile(mapping, destination) {
		var g = gulp.src(BASE_DIR + mapping.src);
		if (mapping.destName) {
			g = g.pipe(rename(mapping.destName));
		}
		var dest = destination;

		if (mapping.destFolder) {
			dest = dest + "/" + mapping.destFolder;
		}

		g.pipe(gulp.dest(dest));
	}

	mappings.forEach(function (mapping) {
		if (mapping.src) {
			copyFile(mapping, JS_DEST);
		}
		else {
			if (mapping.js) {
				mapping.js.forEach(function (jsMapping) {
					copyFile(jsMapping, JS_DEST);
				});
			}
			if (mapping.css) {
				mapping.css.forEach(function (cssMapping) {
					copyFile(cssMapping, CSS_DEST);
				});
			}
		}
	});
});