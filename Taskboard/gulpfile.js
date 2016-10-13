/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var rename = require('gulp-rename');
var modernizr = require('gulp-modernizr');

var BOWER_BASE_DIR = "./wwwroot/lib/";
var NPM_BASE_DIR = "./node_modules/";
var JS_DEST = "./Scripts/thirdparty/";
var CSS_DEST = "./Content/thirdparty/";


var mappings = [
	{ "src": "jquery/dist/jquery.js", "destName": "jquery.js" },
	{
		"js": [{ "src": "jquery-ui/jquery-ui.js" }],
		"js": [{ "src": "signalr/jquery.signalR.js" }],
		"js": [{ "src": "moment/moment.js" }],
		"css": [
			{ "src": "jquery-ui/themes/base/*", destFolder: "jquery-ui" },
			{ "src": "jquery-ui/themes/base/images/*", destFolder: "jquery-ui/images" }
		]
	},
	{ "src": "systemjs/dist/system.js", "npm": true },
	{ "src": "redux/dist/redux.js", "npm": true },
	{ "src": "react-redux/dist/react-redux.js", "npm": true },
	{
		"js": [
			{ "src": "react/react.js" },
			{ "src": "react/react-dom.js" }
		]
	}
];

gulp.task('modernizr', function ()
{
	gulp.src('./Scripts/**.js')
		.pipe(modernizr())
		.pipe(gulp.dest(JS_DEST));
});

gulp.task('default', function () {
	function copyFile(mapping, destination, baseDir) {
		var g = gulp.src(baseDir + mapping.src);
		if (mapping.destName) {
			g = g.pipe(rename(mapping.destName));
		}
		var dest = destination;

		if (mapping.destFolder) {
			dest = dest + "/" + mapping.destFolder;
		}

		g.pipe(gulp.dest(dest));
	}

	mappings.forEach(function (mapping)
	{
		var baseDir = BOWER_BASE_DIR;
		if (mapping.npm)
		{
			baseDir = NPM_BASE_DIR;
		}

		if (mapping.src) {
			copyFile(mapping, JS_DEST, baseDir);
		}
		else {
			if (mapping.js) {
				mapping.js.forEach(function (jsMapping) {
					copyFile(jsMapping, JS_DEST, baseDir);
				});
			}
			if (mapping.css) {
				mapping.css.forEach(function (cssMapping) {
					copyFile(cssMapping, CSS_DEST, baseDir);
				});
			}
		}
	});
});