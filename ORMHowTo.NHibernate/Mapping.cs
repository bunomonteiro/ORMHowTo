using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using ORMHowTo.Infrastructure;

namespace ORMHowTo.NHibernate
{
    public class ArtistMap : ClassMapping<Artist>
    {
        public ArtistMap()
        {
            Table("Artist");

            Lazy(true);

            Id(x => x.ArtistId, map => map.Generator(Generators.Native));

            Property(x => x.Name);

            // OneToMany
            Bag(
                x => x.Albums, 
                map =>
                {
                    map.Key(k => k.Column("ArtistId"));
                },
                rel => rel.OneToMany(m => m.Class(typeof(Album)))
            );
        }
    }

    public class AlbumMap : ClassMapping<Album>
    {
        public AlbumMap()
        {
            Table("Album");

            Lazy(true);

            Id(x => x.AlbumId, map => map.Generator(Generators.Native));

            Property(x => x.Title, map => map.NotNullable(true));
            Property(x => x.ArtistId, map => map.NotNullable(true));

            ManyToOne(x => x.Artist, m =>
            {
                m.Column("ArtistId");
                m.Cascade(Cascade.All);
            });

            // OneToMany
            Bag(
                x => x.Tracks,
                map =>
                {
                    map.Key(k => k.Column("AlbumId"));
                },
                rel => rel.OneToMany(m => m.Class(typeof(Track)))
            );
        }
    }

    public class CustomerMap : ClassMapping<Customer>
    {
        public CustomerMap()
        {
            Table("Customer");

            Lazy(true);

            Id(x => x.CustomerId, map => map.Generator(Generators.Native));

            Property(x => x.FirstName, map => map.NotNullable(true));
            Property(x => x.LastName, map => map.NotNullable(true));
            Property(x => x.Company);
            Property(x => x.Address);
            Property(x => x.City);
            Property(x => x.State);
            Property(x => x.Country);
            Property(x => x.PostalCode);
            Property(x => x.Phone);
            Property(x => x.Fax);
            Property(x => x.Email, map => map.NotNullable(true));
            Property(x => x.SupportRepId);

            ManyToOne(x => x.SupportRep, m =>
            {
                m.Column("SupportRepId");
                m.Cascade(Cascade.All);
            });

            // OneToMany
            Bag(
                x => x.Invoices,
                map =>
                {
                    map.Key(k => k.Column("CustomerId"));
                },
                rel => rel.OneToMany(m => m.Class(typeof(Invoice)))
            );
        }
    }

    public class EmployeeMap : ClassMapping<Employee>
    {
        public EmployeeMap()
        {
            Table("Employee");

            Lazy(true);

            Id(x => x.EmployeeId, map => map.Generator(Generators.Native));

            Property(x => x.LastName, map => map.NotNullable(true));
            Property(x => x.FirstName, map => map.NotNullable(true));
            Property(x => x.Title);
            Property(x => x.ReportsTo);
            Property(x => x.BirthDate);
            Property(x => x.HireDate);
            Property(x => x.Address);
            Property(x => x.City);
            Property(x => x.State);
            Property(x => x.Country);
            Property(x => x.PostalCode);
            Property(x => x.Phone);
            Property(x => x.Fax);
            Property(x => x.Email);

            ManyToOne(x => x.Manager, m =>
            {
                m.Column("ReportsTo");
                m.Cascade(Cascade.All);
            });

            // OneToMany
            Bag(
                x => x.Subordinates,
                map =>
                {
                    map.Key(k => k.Column("ReportsTo"));
                },
                rel => rel.OneToMany(m => m.Class(typeof(Employee)))
            );

            // OneToMany
            Bag(
                x => x.Customers,
                map =>
                {
                    map.Key(k => k.Column("EmployeeId"));
                },
                rel => rel.OneToMany(m => m.Class(typeof(Customer)))
            );
        }
    }

    public class GenreMap : ClassMapping<Genre>
    {
        public GenreMap()
        {
            Table("Genre");

            Lazy(true);

            Id(x => x.GenreId, map => map.Generator(Generators.Native));

            Property(x => x.Name);

            // OneToMany
            Bag(
                x => x.Tracks,
                map =>
                {
                    map.Key(k => k.Column("GenreId"));
                },
                rel => rel.OneToMany(m => m.Class(typeof(Track)))
            );
        }
    }

    public class InvoiceLineMap : ClassMapping<InvoiceLine>
    {
        public InvoiceLineMap()
        {
            Table("InvoiceLine");

            Lazy(true);

            Id(x => x.InvoiceLineId, map => map.Generator(Generators.Native));

            Property(x => x.InvoiceId, map => map.NotNullable(true));
            Property(x => x.TrackId, map => map.NotNullable(true));
            Property(x => x.UnitPrice, map => map.NotNullable(true));
            Property(x => x.Quantity, map => map.NotNullable(true));

            ManyToOne(x => x.Invoice, m =>
            {
                m.Column("InvoiceId");
                m.Cascade(Cascade.All);
            });

            ManyToOne(x => x.Track, m =>
            {
                m.Column("TrackId");
                m.Cascade(Cascade.All);
            });
        }
    }

    public class InvoiceMap : ClassMapping<Invoice>
    {
        public InvoiceMap()
        {
            Table("Invoice");

            Lazy(true);

            Id(x => x.InvoiceId, map => map.Generator(Generators.Native));

            Property(x => x.CustomerId, map => map.NotNullable(true));
            Property(x => x.InvoiceDate, map => map.NotNullable(true));
            Property(x => x.BillingAddress);
            Property(x => x.BillingCity);
            Property(x => x.BillingState);
            Property(x => x.BillingCountry);
            Property(x => x.BillingPostalCode);
            Property(x => x.Total, map => map.NotNullable(true));

            ManyToOne(x => x.Customer, m =>
            {
                m.Column("CustomerId");
                m.Cascade(Cascade.All);
            });

            // OneToMany
            Bag(
                x => x.InvoiceLines,
                map =>
                {
                    map.Key(k => k.Column("InvoiceId"));
                },
                rel => rel.OneToMany(m => m.Class(typeof(InvoiceLine)))
            );
        }
    }

    public class MediaTypeMap : ClassMapping<MediaType>
    {
        public MediaTypeMap()
        {
            Table("MediaType");

            Lazy(true);

            Id(x => x.MediaTypeId, map => map.Generator(Generators.Native));

            Property(x => x.Name);

            // OneToMany
            Bag(
                x => x.Tracks,
                map =>
                {
                    map.Key(k => k.Column("MediaTypeId"));
                },
                rel => rel.OneToMany(m => m.Class(typeof(Track)))
            );
        }
    }

    public class PlaylistMap : ClassMapping<Playlist>
    {
        public PlaylistMap()
        {
            Table("Playlist");

            Lazy(true);

            Id(x => x.PlaylistId, map => map.Generator(Generators.Native));

            Property(x => x.Name);

            // ManyToMany
            Bag(playlist =>
                playlist.Tracks,
                map =>
                {
                    map.Table("PlaylistTrack");
                    map.Key(k => k.Column("PlaylistId"));
                },
                rel => rel.ManyToMany(m => m.Column("TrackId"))
            );
        }
    }

    public class PlaylistTrackMap : ClassMapping<PlaylistTrack>
    {
        public PlaylistTrackMap()
        {
            Table("PlaylistTrack");

            Lazy(true);

            ComposedId(compId =>
            {
                compId.Property(x => x.PlaylistId, m => m.Column("PlaylistId"));
                compId.Property(x => x.TrackId, m => m.Column("TrackId"));
            });
        }
    }

    public class TrackMap : ClassMapping<Track>
    {
        public TrackMap()
        {
            Table("Track");

            Lazy(true);

            Id(x => x.TrackId, map => map.Generator(Generators.Native));

            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.AlbumId);
            Property(x => x.MediaTypeId, map => map.NotNullable(true));
            Property(x => x.GenreId);
            Property(x => x.Composer);
            Property(x => x.Milliseconds, map => map.NotNullable(true));
            Property(x => x.Bytes);
            Property(x => x.UnitPrice, map => map.NotNullable(true));

            ManyToOne(x => x.Album, m =>
            {
                m.Column("AlbumId");
                m.Cascade(Cascade.All);
            });

            ManyToOne(x => x.Genre, m =>
            {
                m.Column("GenreId");
                m.Cascade(Cascade.All);
            });

            ManyToOne(x => x.MediaType, m =>
            {
                m.Column("MediaTypeId");
                m.Cascade(Cascade.All);
            });

            // ManyToMany
            Bag(playlist =>
                playlist.Playlists,
                map =>
                {
                    map.Table("PlaylistTrack");
                    map.Key(k => k.Column("TrackId"));
                },
                rel => rel.ManyToMany(m => m.Column("PlaylistId"))
            );
        }
    }
}
