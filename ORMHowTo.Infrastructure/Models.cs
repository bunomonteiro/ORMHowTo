using System;
using System.Collections.Generic;

namespace ORMHowTo.Infrastructure
{
    public class Model { }

    public class Artist : Model
    {
        public virtual int ArtistId { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Album> Albums { get; set; }

        public Artist()
        {
            this.Albums = new List<Album>();
        }
    }

    public class Genre : Model
    {
        public virtual int GenreId { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Track> Tracks { get; set; }

        public Genre()
        {
            Tracks = new List<Track>();
        }
    }

    public class MediaType : Model
    {
        public virtual int MediaTypeId { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Track> Tracks { get; set; }

        public MediaType()
        {
            Tracks = new List<Track>();
        }
    }

    public class Playlist : Model
    {
        public virtual int PlaylistId { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Track> Tracks { get; set; }

        public Playlist()
        {
            this.Tracks = new List<Track>();
        }
    }

    public class Album : Model
    {
        public virtual int AlbumId { get; set; }
        public virtual string Title { get; set; }
        public virtual int ArtistId { get; set; }
        /// <summary>
        /// ForeignKey: ArtistId
        /// </summary>
        public virtual Artist Artist { get; set; }

        public virtual IList<Track> Tracks { get; set; }

        public Album()
        {
            Tracks = new List<Track>();
        }
    }

    public class Employee : Model
    {
        public virtual int EmployeeId { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Title { get; set; }
        public virtual int ReportsTo { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual DateTime HireDate { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        /// <summary>
        /// ForeignKey: ReportsTo
        /// </summary>
        public virtual Employee Manager { get; set; }
        public virtual IList<Employee> Subordinates { get; set; }

        public virtual IList<Customer> Customers { get; set; }

        public Employee()
        {
            this.Customers = new List<Customer>();
            this.Subordinates = new List<Employee>();
        }
    }

    public class Customer : Model
    {
        public virtual int CustomerId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Company { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual int SupportRepId { get; set; }
        /// <summary>
        /// ForeignKey: SupportRepId
        /// </summary>
        public virtual Employee SupportRep { get; set; }

        public virtual IList<Invoice> Invoices { get; set; }

        public Customer()
        {
            this.Invoices = new List<Invoice>();
        }
    }

    public class Invoice : Model
    {
        public virtual int InvoiceId { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual DateTime InvoiceDate { get; set; }
        public virtual string BillingAddress { get; set; }
        public virtual string BillingCity { get; set; }
        public virtual string BillingState { get; set; }
        public virtual string BillingCountry { get; set; }
        public virtual string BillingPostalCode { get; set; }
        public virtual decimal Total { get; set; }
        /// <summary>
        /// ForeignKey: CustomerId
        /// </summary>
        public virtual Customer Customer { get; set; }

        public virtual IList<InvoiceLine> InvoiceLines { get; set; }

        public Invoice()
        {
            this.InvoiceLines = new List<InvoiceLine>();
        }
    }

    public class Track : Model
    {
        public virtual int TrackId { get; set; }
        public virtual string Name { get; set; }
        public virtual int AlbumId { get; set; }
        public virtual int MediaTypeId { get; set; }
        public virtual int GenreId { get; set; }
        public virtual string Composer { get; set; }
        public virtual int Milliseconds { get; set; }                   
        public virtual int Bytes { get; set; }
        public virtual decimal UnitPrice { get; set; }
        /// <summary>
        /// ForeignKey: AlbumId
        /// </summary>
        public virtual Album Album { get; set; }
        /// <summary>
        /// ForeignKey: MediaTypeId
        /// </summary>
        public virtual MediaType MediaType { get; set; }
        /// <summary>
        /// ForeignKey: GenreId
        /// </summary>
        public virtual Genre Genre { get; set; }

        public virtual IList<Playlist> Playlists { get; set; }

        public Track()
        {
            this.Playlists = new List<Playlist>();
        }
    }

    public class InvoiceLine : Model
    {
        public virtual int InvoiceLineId { get; set; }
        public virtual int InvoiceId { get; set; }
        public virtual int TrackId { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual int Quantity { get; set; }
        /// <summary>
        /// ForeignKey: InvoiceId
        /// </summary>
        public virtual Invoice Invoice { get; set; }
        /// <summary>
        /// ForeignKey: TrackId
        /// </summary>
        public virtual Track Track { get; set; }
    }

    public class PlaylistTrack : Model
    {
        public virtual int PlaylistId { get; set; }
        public virtual int TrackId { get; set; }
        /// <summary>
        /// ForeignKey: PlaylistId
        /// </summary>
        public virtual Playlist Playlist { get; set; }
        /// <summary>
        /// ForeignKey: TrackId
        /// </summary>  
        public virtual Track Track { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as PlaylistTrack;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this == other;
        }

        public static bool operator ==(PlaylistTrack playlistTrackA, PlaylistTrack playlistTrackB) 
            => playlistTrackA.PlaylistId == playlistTrackB.PlaylistId
            && playlistTrackA.TrackId == playlistTrackB.TrackId;

        public static bool operator !=(PlaylistTrack playlistTrackA, PlaylistTrack playlistTrackB) 
            => !(playlistTrackA == playlistTrackB);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ PlaylistId.GetHashCode();
                hash = (hash * 31) ^ TrackId.GetHashCode();

                return hash;
            }
        }
    }
}
