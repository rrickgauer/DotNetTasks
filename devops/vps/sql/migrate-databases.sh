./.print-header.sh "Saving existing Tasks data..."
./.export-tasks-data.sh

./.print-header.sh "Copying over the table schemas..."
./.copy-schema.sh

./.print-header.sh "Copying over the enums..."
./.copy-enums.sh

./.print-header.sh "Import the Tasks dump data..."
./.import-tasks-data.sh
