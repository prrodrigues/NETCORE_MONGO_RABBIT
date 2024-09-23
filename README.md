--turning on, MongoDb and RabbitMQ via docker compose
--execute the command below

cd teste/TEST_RENT/Test.RentMotorCycles/AssetsDB/MongoDb-RabbitMQ

sudo service docker start

docker compose up


----------------------------------------------------------------------------------
--Activate the RabbitMQ Consumer Service
cd ~/teste/TEST_RENT/Test.RentMotorCycles/Test.RentMotorCycles.RabbitMQConsumer

dotnet run



----------------------------------------------------------------------------------
--Run the api 
cd ~/teste/TEST_RENT/Test.RentMotorCycles/Teste.RentMotorCycle.Api

dotnet run


----------------------------------------------------------------------------------
--Run Test Project 
cd ~/teste/TEST_RENT/Test.RentMotorCycles/Teste.RentMotorCycle.Tests

dotnet run
