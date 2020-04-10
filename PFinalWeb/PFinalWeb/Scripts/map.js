var mapScript = document.querySelector('#_2kdsaqjh1');
function getCorrectPath(path){
    var origin = window.location.origin
    return origin+path
}

function callScript(script) 
{
    // $.ajax({
    //     url: script,
    //     dataType: "script",
    //     async: true,           
    //     success: function () {
    //         loadMap();
    //     },
    //     error: function () {
    //         throw new Error(`No se pudo cargar el script ${script}`);
    //     }
    // });
}

function insertCSS(url)
{
    // var linkCSS = document.createElement('link')
    // linkCSS.rel = "stylesheet"
    // linkCSS.href = url
    // document.head.appendChild(linkCSS);
}

function loadThings()
{   

    // div = document.createElement('div')
    // div.id = 'mapp'
    // document.querySelector('.body-content').insertBefore(div, mapScript)
    hrefs = ['/Resources/leaflet/dist/leaflet.css', '/Resources/leaflet/dist/leaflet-extra-markers.min.css']
    for(i in hrefs){insertCSS(getCorrectPath(hrefs[i]))}

    srcs = ['/Resources/leaflet/dist/leaflet.js', '/Resources/leaflet/dist/leaflet-extra-markers.min.js']
    for(i in srcs){
        callScript(getCorrectPath(srcs[i]))
    }
    loadMap();
}

const loadMap = async function()
{
    preLoader(false);
    mapContainer = document.querySelector('#mapp');
    
    if(mapContainer != null)
    {
        var data;
        try {
            const resp = await fetch(location.origin + '/Coords.asmx/GetInfoCaso', { method: 'post' })
            data = await resp.json();
        } catch (errs) {
            console.log(errs)
        }

        mapContainer.innerHTML = "<div id='map' style='width: 100%; height: 100%;'></div>";
        mapContainer.setAttribute("style", `height:490px;width:100%;margin:0px auto;margin-top:30px;margin-bottom:30px;`)
        map = L.map('map').setView([18.4666666667, -69.9], 7.23);
                                    //LAT         //LNG
        var tileLayer = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { //carga capa desde leaflet 
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetmap</a> contributors' //y respectiva payola...(es gratis)
        })
        tileLayer.addTo(map);

        for(i in data)
        {
            /*
            data[i][0] => ID CASO
            data[i][1] => NOMBRE
            data[i][2] => APELLIDO
            data[i][3] => LAT
            data[i][4] => LONG
            */
            //coords(lat, long) desde origin/Coords.asmx/GetCoords
            var point = [data[i][3], data[i][4]];
            var marker = L.marker(point).bindPopup(`${data[i][1]} ${data[i][2]}
            <br/><a href="${getCorrectPath(`/Home/Casos/${data[i][0]}`)}">M&aacute;s informaci&oacute;n</a>`);
            marker.addTo(map); //
        }


        // var redMarker = L.ExtraMarkers.icon({
        //     icon: 'fa-exclamation-triangle',
        //     markerColor: 'red',
        //     shape: 'circle',
        //     prefix: 'fas'
        //   }).bindPopup(`Localizaci&oacute;n`);
        // L.marker(point, {icon: redMarker}).addTo(map);
    }
    preLoader(true);
}

function preLoader(eliminar){ //elimar => (bool) true / false
    if(!eliminar){
        let loader = document.createElement('div');
        loader.id = 'SBC-Loader';
            var renderLoader = `
        <div class="progress blue">
        <div class="indeterminate green"></div>
        </div>
        `;
        loader.innerHTML = renderLoader;
        document.querySelector('header').appendChild(loader);
    }else{
        var sbcLoader = document.querySelector('#SBC-Loader')
        if(sbcLoader)
            sbcLoader.remove();
    }
}
loadThings();