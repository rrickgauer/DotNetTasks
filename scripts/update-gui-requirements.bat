:: This script updates the requirements.txt file

set pip=C:\xampp\htdocs\files\DotNetTasks\venv\Scripts\pip

%pip% freeze -l > C:\xampp\htdocs\files\DotNetTasks\src\Gui\requirements.txt

pause