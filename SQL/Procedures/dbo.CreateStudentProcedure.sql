CREATE PROCEDURE DodajStudenta
    @Imie NVARCHAR(50),
    @Nazwisko NVARCHAR(50),
    @IDGrupy INT = NULL
AS
BEGIN
    INSERT INTO Student (Imie, Nazwisko, IDGrupy)
    VALUES (@Imie, @Nazwisko, @IDGrupy);

    SELECT SCOPE_IDENTITY() AS NewStudentID;
END
