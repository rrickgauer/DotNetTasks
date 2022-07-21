:: --------------------------------------------
:: Start up the gui development flask server
:: --------------------------------------------

cd C:\xampp\htdocs\files\DotNetTasks\src\Gui

set flask_program=C:\xampp\htdocs\files\DotNetTasks\venv\Scripts\flask
set FLASK_ENV=development
set FLASK_APP=tasks

%flask_program% run --with-threads --host 0.0.0.0 --port 5020
