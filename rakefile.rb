MSBuild = 'C:\Windows\Microsoft.Net\Framework\v3.5\msbuild.exe'
Projects = ['MuonLab.Commons', 'MuonLab.Validation', 'MuonLab.Validation.Reports', 'MuonLab.Commons.StructureMap', 'MuonLab.NHibernate', 'MuonLab.Testing', 'MuonLab.Web.Mvc', 'MuonLab.Data', 'MuonLab.Testing.Mvc', 'MuonLab.Web.Xhtml', 'MuonLab.Web']

namespace :build do

	desc "Create debug and release builds"
	task :all => [:clean, :debug, :release]

	task :default => :all

	task :clean do |t|
		FileUtils.rm_rf "build/"
		FileUtils.mkdir "build/"
	end

	task :copylibs do |t|
	
		system "svn export \"lib/\" \"build/lib\""
	
	end

	task :debug => [:copylibs] do |t|
	
		Projects.each {|proj|
			system "#{MSBuild} /v:m /t:rebuild /p:configuration=debug;outdir=../../build/debug/ src/#{proj}/#{proj}.csproj"
		}
	
		FileUtils.rm Dir["build/debug/*"].reject {|file| /MuonLab/.match(file)}
		
	end
	
	task :release => [:copylibs] do |t|
	
		Projects.each {|proj|
			system "#{MSBuild} /v:m /t:rebuild /p:configuration=release;outdir=../../build/release/ src/#{proj}/#{proj}.csproj"
		}
		
		FileUtils.rm Dir["build/release/*"].reject {|file| /MuonLab/.match(file)}
	
	end
	
end