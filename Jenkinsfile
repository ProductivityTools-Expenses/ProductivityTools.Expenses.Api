properties([pipelineTriggers([githubPush()])])

pipeline {
    agent any

    stages {
        stage('hello') {
            steps {
                // Get some code from a GitHub repository
                echo 'hello'
            }
        }
        stage('deleteWorkspace') {
            steps {
                deleteDir()
            }
        }
        stage('clone') {
            steps {
                // Get some code from a GitHub repository
                git branch: 'main',
                url: 'https://github.com/ProductivityTools-Expenses/ProductivityTools.Expenses.Api.git'
            }
        }
		stage('deleteDbMigratorDir') {
            steps {
                bat('if exist "C:\\Bin\\DbMigration\\Expenses.Api" RMDIR /Q/S "C:\\Bin\\DbMigration\\Expenses.Api"')
            }
        }
        stage('build') {
            steps {
                bat(script: "dotnet publish ProductivityTools.Expenses.Api.sln -c Release", returnStdout: true)
            }
        }
 
        stage('copyDbMigratdorFiles') {
            steps {           
                bat('xcopy "ProductivityTools.Expenses.DbUp\\bin\\Release\\net6.0\\publish" "C:\\Bin\\DbMigration\\Expenses.Api\\" /O /X /E /H /K')
            }
        }

        stage('runDbMigratorFiles') {
            steps {
                bat('C:\\Bin\\DbMigration\\Expenses.Api\\ProductivityTools.Expenses.DbUp.exe')
            }
        }

        stage('stopMeetingsOnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd stop site /site.name:PTExpenses')
            }
        }
	    stage('Sleep') {
			steps {
				script {
					print('I am sleeping for a while!')
					sleep(30)    
				}
			}
		}
		stage('deleteIisDirFiles') {
            steps {
                retry(5) {
                    bat('if exist "C:\\Bin\\IIS\\PTExpenses" del /q "C:\\Bin\\IIS\\PTExpenses\\*"')
                }

            }
        }
        stage('deleteIisDir') {
            steps {
                retry(5) {
                    bat('if exist "C:\\Bin\\IIS\\PTExpenses" RMDIR /Q/S "C:\\Bin\\IIS\\PTExpenses"')
                }

            }
        }
        stage('copyIisFiles') {
            steps {         
                bat('xcopy "ProductivityTools.Expenses.Api\\bin\\Release\\net8.0\\publish" "C:\\Bin\\IIS\\PTExpenses\\" /O /X /E /H /K')
            }
        }

        stage('startMeetingsOnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd start site /site.name:PTExpenses')
            }
        }
        stage('byebye') {
            steps {
                // Get some code from a GitHub repository
                echo 'byebye1'
            }
        }
    }
	post {
		always {
            emailext body: "${currentBuild.currentResult}: Job ${env.JOB_NAME} build ${env.BUILD_NUMBER}\n More info at: ${env.BUILD_URL}",
                recipientProviders: [[$class: 'DevelopersRecipientProvider'], [$class: 'RequesterRecipientProvider']],
                subject: "Jenkins Build ${currentBuild.currentResult}: Job ${env.JOB_NAME}"
		}
	}
}
