:: This script updates the requirements.txt file

set pip=C:\xampp\htdocs\files\DotNetTasks\src\Gui\.venv\Scripts\pip

cd C:\xampp\htdocs\files\DotNetTasks\src\Gui

%pip% freeze -l > requirements.txt

start notepad "requirements.txt"

exit