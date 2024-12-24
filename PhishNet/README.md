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

    GetJamChartsBySongAsync(string songSlug)
    GetJamChartsByShowIdAsync(long showId)
    GetJamChartsByShowDateAsync(DateOnly showDate)

### Review
    GetReviewsByUserIdAsync(long userId)
    GetReviewsByUsernameAsync(string username)
    GetReviewsByShowIdAsync(long showId)
    GetReviewsByShowDateAsync(DateOnly showDate)

### SetlistItem
    GetPerformancesBySongAsync(string songSlug)
    GetSetlistByShowIdAsync(long showId)
    GetSetlistByShowDateAsync(DateOnly showDate)

### Show
    GetShowsAsync()
    GetShowByIdAsync(long showId)

### Song
    GetSongsAsync()
    GetSongByIdAsync(long songId)

### SongData
    GetSongDataAsync()
    GetSongDataByIdAsync(long songId)

### User
    GetUserByIdAsync(long userId)
    GetUserByUsernameAsync(string username)

### Venue
    GetVenuesAsync()
    GetVenueByIdAsync(int venueId)
