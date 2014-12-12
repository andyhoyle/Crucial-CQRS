module.exports = function(grunt) {
  grunt.loadNpmTasks('grunt-contrib-connect');

  grunt.initConfig({
    connect: {
      server: {
        options: {
          port: 8000,
          base: {
            path: '.',
            options: {
              index: 'index.html',
              maxAge: 300000
            }
          },
          livereload: true,
          keepalive: true
        }
      }
    }
  });
};