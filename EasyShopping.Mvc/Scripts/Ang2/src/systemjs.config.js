/**
 * System configuration for Angular samples
 * Adjust as necessary for your application needs.
 */
(function (global) {
    System.config({
        paths: {
            // paths serve as alias
            'root:': 'Scripts/Ang2/',
            'npm:': 'Scripts/Ang2/node_modules/',
            '@ang:': 'Scripts/Ang2/node_modules/@angular/',

        },
        // map tells the System loader where to look for things
        map: {
            // our app is within the app folder
            'app': 'root:dist/app', // 'dist',

            // angular bundles
            '@angular/core': '@ang:core/bundles/core.umd.js',
            '@angular/common': '@ang:common/bundles/common.umd.js',
            '@angular/compiler': '@ang:compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': '@ang:platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': '@ang:platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': '@ang:http/bundles/http.umd.js',
            '@angular/router': '@ang:router/bundles/router.umd.js',
            '@angular/forms': '@ang:forms/bundles/forms.umd.js',

            // other libraries
            'rxjs': 'npm:rxjs',
            'angular-in-memory-web-api': 'npm:angular-in-memory-web-api/bundles/in-memory-web-api.umd.js',
            'angular2-jwt': 'npm:angular2-jwt',
        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            '.': {
                defaultExtension: 'js'
                //meta: {
                //  './*.js': {
                //    loader: 'systemjs-angular-loader.js'
                //  }
                //}
            },
            'angular2-jwt': { main: 'angular2-jwt.js', defaultExtension: 'js' },
            'rxjs': { defaultExtension: 'js' }
        }
    });
})(this);
