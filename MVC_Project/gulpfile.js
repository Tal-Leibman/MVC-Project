/// <binding AfterBuild='_initAll' ProjectOpened='_initAll' />
var gulp = require('gulp')
var sass = require('gulp-sass')
var concat = require('gulp-concat')
var uglify = require('gulp-uglify')
var cssMini = require('gulp-cssmin')

// Path definitions
var paths = {
	webroot: "./wwwroot/",
	nodeModules: "./node_modules/",
	sass: "./Sass/*.scss"
}

paths.compiledCss = paths.webroot + "styles/"

// Third party JS files
paths.vendorScript = [
	paths.nodeModules + "jquery/dist/jquery.js",
	paths.nodeModules + "popper.js/dist/umd/popper.js",
	paths.nodeModules + "bootstrap/dist/js/bootstrap.js",
]

// Third party CSS files
paths.vendorStyles = [
	paths.nodeModules + "bootstrap/dist/css/bootstrap.css",
]

// Third party destination files
paths.vendorDest = paths.webroot + "vendor-scripts/"
paths.vendorCssDest = paths.webroot + "vendor-styles/"

// Compile Sass files
gulp.task("compile-sass", () => {
	return gulp.src(paths.sass)
		.pipe(sass().on('error', sass.logError))
		.pipe(concat("styles.css"))
		.pipe(cssMini())
		.pipe(gulp.dest(paths.compiledCss))
})

// Move JS files from node modules to wwwroot
gulp.task("load-vendor-scripts", () => {
	return gulp.src(paths.vendorScript)
		.pipe(concat("vendor.js"))
		.pipe(uglify())
		.pipe(gulp.dest(paths.vendorDest))
})

// Move CSS files from node modules to wwwroot
gulp.task("load-vendor-styles", () => {
	return gulp.src(paths.vendorStyles)
		.pipe(concat("vendor.css"))
		.pipe(cssMini())
		.pipe(gulp.dest(paths.vendorCssDest))
})

// Watch the file, and compile when it undergoes modifications
gulp.task("sass-watch", () => {
	gulp.watch(paths.sass, gulp.series("compile-sass"))
})

// Run all the tasks
gulp.task("_initAll", gulp.parallel("compile-sass", "load-vendor-scripts", "load-vendor-styles")) // Run then in whatever order
//gulp.task("_initAll", gulp.series("compile-sass", "load-vendor-scripts", "load-vendor-styles")) // Run them in a specific order



