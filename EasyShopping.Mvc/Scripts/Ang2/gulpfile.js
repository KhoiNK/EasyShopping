const
	del = require("del"),
	cache = require('gulp-cached'),
	debug = require("gulp-debug"),
	gulp = require("gulp"),
    sequence = require('gulp-watch-sequence'),
	sourcemaps = require('gulp-sourcemaps'),
	tsc = require("gulp-typescript"),
	tsProject = tsc.createProject("tsconfig.json"),
	tslint = require('gulp-tslint'),
	watch = require('gulp-watch')
;

const DIST_FOLDER = 'dist';

/**
 * Removes `dist` folder.
 */
gulp.task('clean', function () {
    return del.sync([DIST_FOLDER]);
});


/**
 * Checks coding convention.
 */
const srcToLint = ['src/**/*.ts', '!node_modules/**/*.*'];
let lintCode = function () {
    return gulp.src(srcToLint)
		.pipe(cache('linting'))
		.pipe(tslint({
		    formatter: "verbose"
		}))
		.pipe(tslint.report())
};
gulp.task('tslint', ['clean'], lintCode);
gulp.task('tslint-hot', lintCode);


/**
 * Compiles TypeScript sources and writes to `dist` folder.
 */
const TS_FILES = ['src/**/*.ts', 'typings/**/*.d.ts', '!node_modules/**/*.*'];
let compile = function () {

    var onError = function (err) {
        console.error(err.toString());
        this.emit('end');
    };

    return gulp.src(TS_FILES)
		.on('error', onError)
		.on('failed', onError)
		.pipe(cache('compiling'))
		.pipe(debug())
		.pipe(sourcemaps.init())
		.pipe(tsProject(tsc.reporter.fullReporter(true)))
		.pipe(sourcemaps.write('.'))
		.pipe(gulp.dest(DIST_FOLDER));
};
gulp.task('compile', ['tslint'], compile);
gulp.task('compile-hot', ['tslint-hot'], compile);


/**
 * Copies all resources that are not TypeScript files into `dist` directory.
 */
const RESRC_FILES = ['src/**/*', '!./**/*.ts'];
gulp.task('resources', () => {
    return gulp.src(RESRC_FILES)
		.pipe(cache('resourcing'))
		.pipe(debug())
		.pipe(gulp.dest(DIST_FOLDER));
});


/*
 * Default task which is automatically called by "gulp" command.
 */
gulp.task('default', [
	'clean',
	'tslint',
	'compile',
	'resources']);

/*
 * gulp watch
 */
gulp.task('watch', ['clean', 'tslint', 'compile', 'resources'], () => {
    let queue = sequence(1000); // 1 sec

    watch(RESRC_FILES, {
        name: 'watch-resource',
        emitOnGlob: false
    }, queue.getHandler('resources'));

    watch(TS_FILES, {
        name: 'watch-code',
        emitOnGlob: false
    }, queue.getHandler('tslint-hot', 'compile-hot'));
});

gulp.task('clean-cache', function () {
    cache.caches = {};
});