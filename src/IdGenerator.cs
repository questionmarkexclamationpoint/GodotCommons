using System.Threading;

namespace Commons;

public class IdGenerator {
    private ulong currentId;

    public IdGenerator(ulong startingId = 0) {
        currentId = startingId;
    }

    public ulong NewId => Interlocked.Increment(ref currentId);

    public ulong LastId => Interlocked.Read(ref currentId);
}
