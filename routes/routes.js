const express = require('express');
const router = express.Router();
const vehicle = require('../models/vehicle');
const multer = require('multer')
const { fetchVehicles, fetchVehicle, createVehicle, updateVehicle, deleteVehicle } = require('../controllers/vehiclesController');
const upload = require('../middleware/upload'); // adjust the path as needed

var storage = multer.diskStorage (
    {
        destination: function(req, file, cb){
            cb(null, './uploads');
        },
        filename: function (req, file, cb) {
            cb(null, filr.fieldname + "_" + "_" + file.originalname);
        },
    });

var upload = multer ({
    storage: storage,
}).single("image");

router.post("/add", upload, (req,res) =>{
    const user = new User({
        title: req.body.title,
        body: req.body.body,
        description: req.body.description,
        image: req.file.filename,
    });
    user.save((err) =>{
        if(err){
            res.json({message: err.message, type: 'danger'});
        }else{
            req.session.message = {
                type: 'success',
                message: 'User added successfully'
            };
            res.redirect('/');
        }
    })
}
    
)

router.get('/', fetchVehicles);
router.get('/:id', fetchVehicle);
router.post('/', upload.single('image'), createVehicle); // added upload middleware
router.put('/:id', upload.single('image'), updateVehicle); // added upload middleware
router.delete('/:id', deleteVehicle);

module.exports = router;
