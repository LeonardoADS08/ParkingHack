var mongoose = require("mongoose");
var Keeper = mongoose.model("Keeper");
var Spot = mongoose.model("ParkingSpot");
var helpers = require("./helpers.js");
var messages = require("../messages.js");
module.exports.prueba = function(req,res){
    res.status(200).json({
        message:"funciona :)"
    });
}
module.exports.addKeeper = function(req,res){
    if(!req.body.nombre){
        helpers.handleError({
            statusCode:400,
            message: messages.E10001 // missing obligatory data
        }, res);
        return;
    }
    var keeper = {
        nombre: req.body.nombre
    };
    keeper.telefono= req.body.telefono || "";
    Keeper.create(keeper).then(keeper=>{
        res.status(201).json(keeper);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.getKeepers = function(req, res){
    req.query.active = true;
    Keeper.find(req.query).then(keepers=>{
        res.status(200).json(keepers);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.updateKeeper = function(req,res){
    Keeper.findByIdAndUpdate({_id:req.params.keeperId},{$set:req.body}).then(keeper=>{
        if(!keeper){
            throw({
                statusCode:404,
                message:messages.E10002 // not found
            });
        }
        res.status(204).json();
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.getKeeper =function(req, res){
    Keeper.findById(req.params.keeperId).then(keeper=>{
        if(!keeper){
            throw({
                statusCode:404,
                message:messages.E10002 // not found
            });
        }
        res.status(200).json(keeper);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.deleteKeeper = function(req, res){
    Keeper.findByIdAndUpdate({_id:req.params.keeperId},{$set:{active:false}}).then(keeper=>{
        if(!keeper){
            throw({
                statusCode:404,
                message:messages.E10002 // not found
            });
        }
        res.status(204).json();
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.addPark = function(req, res){
    var keeper = {
        nombre : req.body.nombreKeeper,
        telefono:req.body.telefonoKeeper
    };
    var park = {
        nombre : req.body.nombrePark,
        tipo: req.body.tipo,
        costo: req.body.costo,
        horario : req.body.horario,
        longitude: req.body.longitude,
        latitude: req.body.latitude
    };
    Keeper.create(keeper).then(keeper=>{
        park.encargado = keeper._id;
        return Spot.create(park);
    }).then(park=>{
        res.status(201).json(park);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
    
}