const gulp = require("gulp");
const rimraf = require("rimraf");
const concat = require("gulp-concat");
const uglifycss = require("gulp-uglifycss");
const uglifyjs = require("gulp-uglify-es").default;

gulp.task("clean.css", callback => rimraf("./wwwroot/css/**/*.min.css", callback));

gulp.task("clean.js", callback => rimraf("./wwwroot/js/**/*.min.js", callback));

gulp.task("clean", gulp.series([
    "clean.css",
    "clean.js"
]));

gulp.task("bundle.css", () => gulp
    .src([
        "./wwwroot/css/bootstrap/*.css",
        "./wwwroot/css/font-awesome/*.css",
        "./wwwroot/css/forged-grid/*.css",
        "./wwwroot/css/shared/*.css"
    ])
    .pipe(concat("./wwwroot/css/page/bundle.min.css"))
    .pipe(uglifycss())
    .pipe(gulp.dest(".")));

gulp.task("bundle.js", () => gulp
    .src([
        "./wwwroot/js/forged-grid/*.js",
        "./wwwroot/js/shared/*.js"
    ])
    .pipe(concat("./wwwroot/js/page/bundle.min.js"))
    .pipe(uglifyjs({
        output: {
            comments: /^!/
        }
    }))
    .pipe(gulp.dest(".")));

gulp.task("minify", gulp.series([
    "clean.css",
    "clean.js",
    "bundle.css",
    "bundle.js"
]));

gulp.task("default", gulp.series(["minify"]));
