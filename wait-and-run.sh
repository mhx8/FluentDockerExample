#!/bin/bash
PASSWORD="Password!123"
echo "Waiting for SQL Server to be ready..."
for i in {1..50};
do
    /opt/mssql-tools18/bin/sqlcmd -S database -U sa -P $PASSWORD -C -Q "SELECT 1"
    if [ $? -eq 0 ]
    then
        echo "SQL Server is ready."
        break
    else
        echo "Not ready yet..."
        sleep 1
    fi
done
/opt/mssql-tools18/bin/sqlcmd -S database -U sa -P $PASSWORD -d master -C -i /database/create_database.sql
echo "Database created"

# Create Tables
for file in /database/Tables/*.sql; do
    /opt/mssql-tools18/bin/sqlcmd -S database -U sa -P $PASSWORD -C -d master -i "$file"
done
echo "Tables created"

# Create Views
for file in /database/Views/*.sql; do
    /opt/mssql-tools18/bin/sqlcmd -S database -U sa -P $PASSWORD -C -d master -i "$file"
done
echo "Views created"

# Insert Data
for file in /database/Datas/dbo*.sql; do
    /opt/mssql-tools18/bin/sqlcmd -S database -U sa -P $PASSWORD -C -d master -i "$file"
done
echo "Data inserted"

# Create Stored Procedures
# Create Schemas
# ...