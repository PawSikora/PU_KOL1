CREATE OR ALTER TRIGGER trg_Student_Historia_Audit
ON Student
AFTER UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Historia (Imie, Nazwisko, IDGrupy, TypAkcji, Data)
    SELECT
        deleted.Imie,
        deleted.Nazwisko,
        deleted.IDGrupy,
        'edycja',
        SYSDATETIME()
    FROM deleted
    INNER JOIN inserted ON deleted.ID = inserted.ID
    WHERE
        deleted.Imie    <> ISNULL(inserted.Imie, '') OR
        deleted.Nazwisko<> ISNULL(inserted.Nazwisko, '') OR
        ISNULL(deleted.IDGrupy, -1) <> ISNULL(inserted.IDGrupy, -1);

    INSERT INTO Historia (Imie, Nazwisko, IDGrupy, TypAkcji, Data)
    SELECT
        d.Imie,
        d.Nazwisko,
        d.IDGrupy,
        'usuwanie',
        SYSDATETIME()
    FROM deleted d
    LEFT JOIN inserted i ON d.ID = i.ID
    WHERE i.ID IS NULL;
END