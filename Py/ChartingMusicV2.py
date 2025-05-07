import librosa
import json
import argparse
import random
import os

def normalize_weights(weights):
    total = sum(weights.values())
    return {k: v / total for k, v in weights.items()}

def pick_pattern(weight_table):
    return random.choices(
        population=list(weight_table.keys()),
        weights=list(weight_table.values()),
        k=1
    )[0]

def filter_density(onsets, difficulty):
    if difficulty == 'easy':
        return onsets[::3]
    elif difficulty == 'normal':
        return onsets[::2]
    else:
        return onsets  # hard = full

def generate_notes(onset_times, weights, num_lanes=3):
    notes = []
    sections = []

    i = 0
    while i < len(onset_times):
        t = round(float(onset_times[i]), 3)
        pattern = pick_pattern(weights)

        if pattern == "single":
            line = random.randint(0, num_lanes - 1)
            notes.append(f"// Single Note")
            notes.append({"time": t, "line": line})
            i += 1

        elif pattern == "double":
            lines = random.sample(range(num_lanes), 2)
            notes.append(f"// Double Note")
            notes.append({"time": t, "line": lines[0]})
            notes.append({"time": t, "line": lines[1]})
            i += 1

        elif pattern == "long":
            line = random.randint(0, num_lanes - 1)
            duration = round(random.uniform(0.5, 1.5), 2)
            notes.append(f"// Long Note")
            notes.append({"time": t, "line": line, "duration": duration})
            i += 1

        elif pattern == "trill":
            notes.append(f"// Trill")
            lanes = random.sample(range(num_lanes), 2)
            gap = 0.2
            for j in range(4):
                notes.append({"time": round(t + j * gap, 3), "line": lanes[j % 2]})
            i += 4

        elif pattern == "stream":
            notes.append(f"// Stream")
            lane = random.randint(0, num_lanes - 1)
            gap = 0.15
            for j in range(5):
                notes.append({"time": round(t + j * gap, 3), "line": lane})
            i += 5

    return notes

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("audio", help="Input audio file (.wav or .mp3)")
    parser.add_argument("--out", default="chart.json", help="Output JSON path")
    parser.add_argument("--difficulty", default="normal", choices=["easy", "normal", "hard"])
    parser.add_argument("--single", type=int, default=40)
    parser.add_argument("--double", type=int, default=20)
    parser.add_argument("--long", type=int, default=10)
    parser.add_argument("--trill", type=int, default=15)
    parser.add_argument("--stream", type=int, default=15)

    args = parser.parse_args()

    # Load audio
    y, sr = librosa.load(args.audio, sr=None)
    onset_frames = librosa.onset.onset_detect(y=y, sr=sr, backtrack=True)
    onset_times = librosa.frames_to_time(onset_frames, sr=sr)
    onset_times = filter_density(onset_times, args.difficulty)

    # Pattern weights
    weight_table = normalize_weights({
        "single": args.single,
        "double": args.double,
        "long": args.long,
        "trill": args.trill,
        "stream": args.stream
    })

    # Generate notes
    notes = generate_notes(onset_times, weight_table)

    # Build final JSON
    chart = {
        "songName": os.path.splitext(os.path.basename(args.audio))[0],
        "difficulty": args.difficulty,
        "notes": notes
    }

    # Output with comments
    with open(args.out, "w", encoding="utf-8") as f:
        f.write(f"// Chart generated from {args.audio}\n")
        f.write(f"// Difficulty: {args.difficulty}\n")
        f.write(f"[\n")
        for entry in chart["notes"]:
            if isinstance(entry, str):
                f.write(f'  {entry}\n')
            else:
                f.write(f'  {json.dumps(entry)},\n')
        f.write("]\n")

    print(f"✅ 채보 생성 완료: {args.out}")

if __name__ == "__main__":
    main()
