var express = require('express');
var router = express.Router();
var ctrlUser = require("../controllers/user-controller.js");
var ctrlKeeper = require("../controllers/keeper-controller.js");
router.route("/prueba").get(ctrlUser.prueba);

//USER
router.route("/user")
.post(ctrlUser.addUser)
.get(ctrlUser.getUsers);

router.route("/user/:userId")
.get(ctrlUser.getUser)
.put(ctrlUser.updateUser)
.delete(ctrlUser.deleteUser);

//KEEPER
router.route("/keeper")
.post(ctrlKeeper.addKeeper)
.get(ctrlKeeper.getKeepers);

router.route("/keeper/:keeperId")
.get(ctrlKeeper.getKeeper)
.put(ctrlKeeper.updateKeeper)
.delete(ctrlKeeper.deleteKeeper);
module.exports = router;