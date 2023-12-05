// NIX PILLE

job("Example shell script") {
  	parameters {
      	text("CI_PROJECT_DIR", value = ".")
		text("UPM_CACHE_ROOT", value = ".upm")
		text("UNITY_IMAGE_VERSION", value = "3")
		text("UNITY_MODULE", value = "il2cpp")
		text("UNITY_IMAGE_PREFIX", value = "unityci/editor")
		text("UNITY_EXECUTABLE", value = "C:\Program Files\Unity\Hub\Editor\$UNITY_VERSION\Editor\Unity.exe")
		text("BUILD_COMMAND", value = "BuildCommand.PerformBuild")
		text("BUILD_TARGET", value = "Win64")
		text("BUILD_NAME", value = "Monstrum_intus")
		text("BUILD_PATH", value = "dist/")
    }
    container(displayName = "Say Hello", image = "{{BUILD-IMAGE}}") {
        
      	shellScript {
            content = """
              	apt install zip
            	
              	try {
                  	& {{ UNITY_EXECUTEABLE }} `
                  	-projectPath {{ CI_PROJECT_DIR }} `
                  	-quit `
                  	-batchmode `
                  	-nographics `
                  	-buildTarget {{ BUILD_TARGET }} `
                  	-executeMethod {{ BUILD_COMMAND }} `
                  	-logFile - `
                  	-username ${'$'}UNITY_USERNAME `
                  	-password ${'$'}UNITY_PASSWORD `
                  	-serial ${'$'}UNITY_SERIAL `
                  	| Out-Host
              	}
              	finally {
                  	& {{ UNITY_EXECUTEABLE }} `
						-quit \
                  		-batchmode \
                  		-projectpath {{ CI_PROJECT_DIR }} \
                  		-buildWindowsPlayer {{ BUILD_PATH }} \
                  	| Out-Host
              	}
				zip -r -9 {{ BUILD_NAME }}_${${'$'}JB_SPACE_EXECUTION_NUMBER}.zip dist/
            """
        }
    }
    container("amazoncorretto:17-alpine") {
    kotlinScript { api ->
        api.space().projects.automation.deployments.start(
            project = api.projectIdentifier(),
            targetIdentifier = TargetIdentifier.Key("build-game-jam"),
            version = "1.0.0",
            // automatically update deployment status based on a status of a job
            syncWithAutomationJob = true
        )
    }
}
}