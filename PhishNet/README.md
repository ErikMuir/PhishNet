# PhishNet
A C#/.NET wrapper around the [Phish.net](https://phish.net) [API](https://docs.phish.net/) ...and yet another page in the Helping Friendly Book!

## Installation
Add the NuGet package to your project:
```bash
$ dotnet add package PhishNet
```

## Setup
Create a `.env` file in the root of your project and add the following line, replacing `your-api-key` with your actual API key:
```env
PHISH_NET_API_KEY=your-api-key
```

## Usage
Create an instance of the `PhishNetClient` class, and use it to make requests to the Phish.net API.
```csharp
var client = new PhishNetClient();
```
```csharp
var setlist = await client.GetSetlistByShowDateAsync(DateOnly.Parse("1998-07-29"));
```

## Models and Methods

### Artist
    GetArtistsAsync()
    GetArtistByIdAsync(artistId)

### Attendance
    GetAttendanceByUserIdAsync(long userId)
    GetAttendanceByUsernameAsync(string username)
    GetAttendanceByShowIdAsync(long showId)
    GetAttendanceByShowDateAsync(DateOnly showDate)

### JamChart
_(NOTE: JamCharts appear to be the same as SetlistItems)_

    GetJamChartsBySongAsync(songSlug)
    GetJamChartsByShowIdAsync(showId)
    GetJamChartsByShowDateAsync(showDate)

### Review
    GetReviewsByUserIdAsync(userId)
    GetReviewsByUsernameAsync(username)
    GetReviewsByShowIdAsync(showId)
    GetReviewsByShowDateAsync(showDate)

### SetlistItem
    GetPerformancesBySongAsync(songSlug)
    GetSetlistByShowIdAsync(showId)
    GetSetlistByShowDateAsync(showDate)

### Show
    GetShowsAsync()
    GetShowByIdAsync(showId)

### Song
    GetSongsAsync()
    GetSongByIdAsync(songId)

### SongData
    GetSongDataAsync()
    GetSongDataByIdAsync(songDataId)

### User
    GetUserByIdAsync(userId)
    GetUserByUsernameAsync(username)

### Venue
    GetVenuesAsync()
    GetVenueByIdAsync(venueId)
