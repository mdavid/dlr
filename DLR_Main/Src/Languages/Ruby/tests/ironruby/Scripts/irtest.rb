def banner(msg, size=80, char="=")
  border = char * size
  puts border, "#{char*2} #{msg}", border
end

def error?(msg = "At least 1 of the dev unit tests failed")
  unless $?.success?
    puts "",msg
    exit 1
  end
end

flags = ["", "/partial", "/interpret", "/partial /interpret"]

ENV['ROWAN_BIN'] ||= "#{ENV['MERLIN_ROOT']}\\bin\\debug"

flags.each do |flag|
  cmd = "IronRuby.Tests.exe #{flag}"
  banner cmd
  Dir.chdir(ENV['ROWAN_BIN']) do
    exit 1 unless system cmd
  end
end

puts "OK"
