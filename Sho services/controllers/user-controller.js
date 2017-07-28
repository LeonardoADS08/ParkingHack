var mongoose = require("mongoose");
var User = mongoose.model("User");
var helpers = require("./helpers.js");
var messages = require("../messages.js")
module.exports.prueba = function(req,res){
    res.status(200).json({
        message:"funciona :)"
    });
}
module.exports.addUser = function(req,res){
    if(!req.body.nombre){
        helpers.handleError({
            statusCode:400,
            message: messages.E10001 // missing obligatory data
        }, res);
        return;
    }
    var user = {
        nombre: req.body.nombre
    };
    user.idFacebook = req.body.idFacebook || "";
    user.placa = req.body.placa || "";
    user.telefono= req.body.telefono || "";
    user.nombre = req.body.nombre || "";
    User.create(user).then(user=>{
        res.status(201).json(user);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.getUsers = function(req, res){
    req.query.active = true;
    User.find(req.query).then(users=>{
        res.status(200).json(users);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.updateUser = function(req,res){
    User.findByIdAndUpdate({_id:req.params.userId},{$set:req.body}).then(user=>{
        if(!user){
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
module.exports.getUser =function(req, res){
    User.findById(req.params.userId).then(user=>{
        if(!user){
            throw({
                statusCode:404,
                message:messages.E10002 // not found
            });
        }
        res.status(200).json(user);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.deleteUser = function(req, res){
    User.findByIdAndUpdate({_id:req.params.userId},{$set:{active:false}}).then(user=>{
        if(!user){
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