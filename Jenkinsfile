pipeline {
  agent any
 
  tools {nodejs "NodeJSLocal"}
 
  stages {
    stage('Git Checkout') {
        steps {
            git branch: 'Live', url: 'https://github.com/dotnetcrafted/cs-go-statistics.git'
        }
    }
    stage('Frontned') {
      steps {
        bat 'yarn --cwd ./CsStat.Web/ install'
        bat 'yarn --cwd ./CsStat.Web/ run prod'
      }
    }
    stage('Publish') {
      steps {
          bat 'rmdir "C:\\inetpub\\wwwroot\\dist" /s /q'
          bat'xcopy "%WORKSPACE%\\CsStat.Web\\dist" "C:\\inetpub\\wwwroot\\dist" /s /e /y /i'
      }
    }
  }
}
