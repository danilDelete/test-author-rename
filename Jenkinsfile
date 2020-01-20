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
    stage('Backend') {
      steps {
          bat 'C:\\ForBuilding\\nuget.exe restore "%WORKSPACE%/CounterStrikeStat.sln'
          bat '\"${tool 'MSBuildLocal'}\" CounterStrikeStat.sln /p:Configuration=Debug /p:Platform=\"Any CPU\"'
      }
    stage('Publish') {
      steps {
          bat 'rmdir "C:\\inetpub\\wwwroot\\dist" /s /q'
          bat 'xcopy "%WORKSPACE%\\CsStat.Web\\dist" "C:\\inetpub\\wwwroot\\dist" /s /e /y /i'
          bat 'xcopy "%WORKSPACE%\\CsStat.Web\\bin" C:\\inetpub\\wwwroot\\bin\\ /f /s /j /z /d /q /y'
          bat 'xcopy "%WORKSPACE%\\CsStat.Web\\views" C:\\inetpub\\wwwroot\\views\\ /f /s /j /z /d /q /y'
          bat 'xcopy "%WORKSPACE%\\CsStat.Web\\favicon.ico" C:\\inetpub\\wwwroot /j /z /d /q /y'
          bat 'xcopy "%WORKSPACE%\\CsStat.Web\\web.config" C:\\inetpub\\wwwroot  /j /z /d /q /y'
      }
    }
  }
}
