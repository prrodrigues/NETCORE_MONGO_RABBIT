db.createUser(
    {
        user: "root",
        pwd: "root",
        roles: [
            {
                role: "readWrite",
                db: "testDB"
            }
        ]
    }
);

db = new Mongo().getDB("testDB");

db.createCollection('Entregador', { capped: false });
db.createCollection('Moto', { capped: false });
db.createCollection('Locacao', { capped: false });
db.createCollection('MotoEvento', { capped: false });
db.createCollection('Plano', { capped: false });


db.Plano.insertMany([
    {plano:1, quantidade_dias:7, diaria:30.00},
    {plano:2, quantidade_dias:15, diaria:28.00},
    {plano:3, quantidade_dias:30, diaria:22.00},
    {plano:4, quantidade_dias:45, diaria:20.00},
    {plano:5, quantidade_dias:50, diaria:18.00}
]);