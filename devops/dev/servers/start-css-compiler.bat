:: --------------------------------------------
:: Start up the sass compiler
:: --------------------------------------------

cd C:\xampp\htdocs\files\DotNetTasks\src\Gui\tasks\static\css

::sass --watch custom/style.scss dist/custom/style.css
sass --watch custom/style.scss:dist/custom/style.css custom/custom-bootstrap.scss:dist/custom/bootstrap.css

