:: --------------------------------------------
:: Start up the gui development flask server
:: --------------------------------------------

cd C:\xampp\htdocs\files\DotNetTasks\src\Gui

set flask_program=C:\xampp\htdocs\files\DotNetTasks\src\Gui\.venv\Scripts\flask
set FLASK_APP=tasks

%flask_program% --debug run --with-threads --host 0.0.0.0 --port 5020
