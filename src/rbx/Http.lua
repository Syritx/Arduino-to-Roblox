local httpService = game.HttpService
local part = game.Workspace:WaitForChild("HttpBlock")

while true do
	
	local success, error = pcall(function() 
		local response = httpService:GetAsync('http://127.0.0.1:5000/')
		local data = httpService:JSONDecode(response)
		local isHovered = data['hovered']

		if isHovered == false then
			part.Material = Enum.Material.Plastic
		elseif isHovered == true then
			part.Material = Enum.Material.Neon
		end
	end)
	
	if success == false then
		print(error)
	end
	wait(.1)
end