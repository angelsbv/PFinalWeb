let mapContainer = document.querySelector('#map');
mapContainer.setAttribute("style", `height: 490px;width: 100%;margin:0px auto;margin-top:10px;margin-bottom: 30px;`)
if(mapContainer != null)
{
    let map = L.map('map').setView([item.lat, item.long], 7.23);
    
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetmap</a> contributors'
    }).addTo(map);

    var point = [item.lat, item.long];
    var marker = L.marker(point);
    marker.addTo(map);
    marker.bindPopup(
            `${item.firstName} ${item.lastName}`
    )
}