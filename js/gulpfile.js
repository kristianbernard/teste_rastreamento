vargulp = require(‘gulp’);

varsass = require(‘gulp - sass’);

gulp.task(‘sass’, function () {

    gulp.src(‘./css/ *.scss’)

        .pipe(sass())

        .pipe(gulp.dest(function (f) {

            return (f.base +”\CSSFile”);

}))

});



gulp.task(‘default ’, [‘sass’], function () {

    gulp.watch(‘./css/ * scss’, [‘sass’]);

})